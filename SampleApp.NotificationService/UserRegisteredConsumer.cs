using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.NotificationService
{
    public class UserRegisteredConsumer
    {
        public void Consume(IUserRegisteredEvent registeredEvent)
        {
            // Send notification to user
            Console.WriteLine($"Customer notification sent: UserId {registeredEvent.UserId} registered");
        }
    }
}



