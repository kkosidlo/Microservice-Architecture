using Newtonsoft.Json;
using RabbitMQ.Client;
using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.RegistrationService
{
    public class RabbitMqManager : IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory =
                new ConnectionFactory
                {
                    Uri = new Uri(Constants.RabbitMqUri)
                };

            var connection = connectionFactory.CreateConnection();

            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForRegisterUserCommand()
        {
            channel.QueueDeclare(
                queue: Constants.RegisterUserQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);


            //prefetchCount - get any number of messages from the queue 
            //prefetchSize - maximum size of all messages you are willing to receive (0 means no limit)
            //global - connection for this channel only or global 

            channel.BasicQos(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false);

            var consumer = new RegisteredUserCommandConsumer(this);


            // autoAck - message acknowledgment is not needed.
            channel.BasicConsume(
                queue: Constants.RegisterUserQueue,
                consumer: consumer,
                autoAck: false);
        }

        public void SendUserRegisteredEvent(IUserRegisteredEvent command)
        {
            // exchange type: fanout - send a message to multiple queues

            channel.ExchangeDeclare(
                exchange: Constants.UserRegisteredExchange,
                type: ExchangeType.Fanout );

            channel.QueueDeclare(
                queue: Constants.UserRegisteredNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.QueueBind(
                queue: Constants.UserRegisteredNotificationQueue,
                exchange: Constants.UserRegisteredExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();

            messageProperties.CorrelationId = Guid.NewGuid().ToString();

            messageProperties.ContentType = Constants.JsonMimeType;

            channel.BasicPublish(
                exchange: Constants.UserRegisteredExchange,
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        //multiple - decides whether the ack is only for this message

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void Dispose()
        {
            if (!channel.IsClosed) 
                channel.Close();
        }
    }
}
