using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Messaging;
using Newtonsoft.Json;

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


            // Since the value is being sent to the queue as a byte array we first get this byte array and using getstring method change it into string
            var message = Encoding.UTF8.GetString(body);

            //Deserialization of string into strongly typed object.


            // Change the type of project to .netCore for all services.



        }
    }
}
