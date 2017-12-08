using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SampleApp.Messaging;
using SampleApp.NotificationService.Messages;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.NotificationService
{
    public class RabbitMqManager : IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = new Uri(Constants.RabbitMqUri) };
            var connection = connectionFactory.CreateConnection();

            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForOrderRegisteredEvent()
        {
            channel.QueueDeclare(
                queue: Constants.UserRegisteredNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var eventingConsumer = new EventingBasicConsumer(channel);

            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;

                if (contentType != Constants.JsonMimeType)
                    throw new ArgumentException(
                        $"(Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                var userConsumer = new UserRegisteredConsumer();

                var commandObj =
                    JsonConvert.DeserializeObject<UserRegisteredEvent>(message);

                userConsumer.Consume(commandObj);

                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag,
                                multiple: false);
            };

            channel.BasicConsume(
                queue: Constants.UserRegisteredNotificationQueue,
                autoAck: false,
                consumer: eventingConsumer
                );
        }


        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}



