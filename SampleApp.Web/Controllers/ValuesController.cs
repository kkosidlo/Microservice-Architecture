using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Web.ViewModels;

namespace SampleApp.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("Test")]
        public void TestMethod()
        {
            var registerUserCommand = new Messages.RegisterUserCommand(
                new UserViewModel
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Address = "Street 23",
                    Age = 23,
                    IdNumber = 2323232
                });

            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.SendRegisterUserCommand(registerUserCommand);
            }
        }
    }
}
