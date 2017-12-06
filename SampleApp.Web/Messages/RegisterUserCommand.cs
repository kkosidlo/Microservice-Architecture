using SampleApp.Messaging;
using SampleApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Web.Messages
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        private UserViewModel _viewModel;

        public RegisterUserCommand(UserViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public string FirstName => _viewModel.FirstName;
        public string LastName => _viewModel.LastName;
        public string Address => _viewModel.Address;
        public int Age => _viewModel.Age;
        public int IdNumber => _viewModel.IdNumber;
    }
}
