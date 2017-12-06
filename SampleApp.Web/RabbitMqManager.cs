using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.Messaging;
using Newtonsoft.Json;
using System.Text;

namespace SampleApp.Web
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

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }

        public void SendRegisterUserCommand(IRegisterUserCommand command)
        {

            // Exchange creation (do nothing if already exists)

            channel.ExchangeDeclare(
                exchange: Constants.RegisterUserExchange,
                type: ExchangeType.Direct);

            // Queue creation (do nothing if already exists)


            // durable : queue exists only in memory only and it will not persist to disc
            // exclusive : created queue exclusive for this connection
            // autoDelete : automatic delete of queue when the last consumer closes its channel
            // arguments: advance queue creation (out of scope for now)

            channel.QueueDeclare(
                queue: Constants.RegisterUserQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            // Bind between exchange-queue creatation (do nothing if already exists)

            channel.QueueBind(
                queue: Constants.RegisterUserQueue,
                exchange: Constants.RegisterUserExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();

            messageProperties.ContentType = Constants.JsonMimeType;

            channel.BasicPublish(
                exchange: Constants.RegisterUserExchange,
                basicProperties: messageProperties,
                routingKey: "",
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }
    }
}
