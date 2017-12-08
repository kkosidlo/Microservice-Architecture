using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.NotificationService.Messages
{
    public class UserRegisteredEvent : IUserRegisteredEvent
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int IdNumber { get; set; }
    }
}
