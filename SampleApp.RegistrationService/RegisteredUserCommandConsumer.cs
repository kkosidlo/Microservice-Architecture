using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Messaging;
using Newtonsoft.Json;
using SampleApp.RegistrationService.Messages;
using SampleApp.Web.Messages;

namespace SampleApp.RegistrationService
{
    public class RegisteredUserCommandConsumer : DefaultBasicConsumer
    {
        private readonly RabbitMqManager _rabbitMqManager;

        public RegisteredUserCommandConsumer(RabbitMqManager rabbitMqManager)
        {
            this._rabbitMqManager = rabbitMqManager;
        }

        public override void HandleBasicDeliver(
            string consumerTag, ulong deliveryTag,
            bool redelivered, string exchange,
            string routingKey, IBasicProperties properties,
            byte[] body)
        {
            if (properties.ContentType != Constants.JsonMimeType)
                throw new ArgumentException(
                    $"Can't handle content type { properties.ContentType }");

            var message = Encoding.UTF8.GetString(body);
            var commandObj =
                JsonConvert.DeserializeObject<RegisterUserCommand>(
                    message);

            Consume(commandObj);
            _rabbitMqManager.SendAck(deliveryTag);

        }

        private void Consume(IRegisterUserCommand command)
        {
            // Store order registration and get id 
            int id = 12;

            Console.WriteLine($"Order with id {id} registered");
            Console.WriteLine("Publishing order registered event");

            //notify subscribers that a order is registered
            var orderRegisteredEvent = new UserRegisteredEvent(command, id);
            //publish event
            _rabbitMqManager.SendUserRegisteredEvent(orderRegisteredEvent);
        }
    }
}
 

