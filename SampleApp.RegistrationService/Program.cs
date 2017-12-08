using System;

namespace SampleApp.RegistrationService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration Service";

            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.ListenForRegisterUserCommand();
                Console.WriteLine("Listening for RegisterUserCommand.");
                Console.ReadKey();
            }
        }
    }
}
