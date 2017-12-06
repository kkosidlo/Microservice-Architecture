using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Messaging
{
    public static class Constants
    {
        public const string RabbitMqUri = "amqp://guest:guest@localhost:5672/";
        public const string JsonMimeType = "application/json";
        public const string RegisterUserExchange = "user.userregister.exchange";
        public const string RegisterUserQueue = "user.userregister.queue";
        public const string UserRegisteredExchange = "user.userregistered.exchange";
        public const string UserRegisteredNotificationQueue = "user.userregistered.notification.queue";

    }
}