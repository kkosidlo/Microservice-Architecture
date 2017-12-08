using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.RegistrationService.Messages
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int IdNumber { get; set; }
    }
}
