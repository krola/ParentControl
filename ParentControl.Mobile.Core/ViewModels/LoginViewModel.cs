using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Events.Authorization;
using System;

namespace ParentControl.Mobile.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private MvxCommand _loginCommand;
        private IAccountService _accountService;

        public event EventHandler<UnauthorizedEvent> OnUnauthorized;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public MvxCommand LoginCommand
        {
            get
            {
                _loginCommand = _loginCommand ?? new MvxCommand(LoginActionAsync);
                return _loginCommand;
            }
        }

        public async void LoginActionAsync()
        {
            var result = await _accountService.AuthenticateAsync(Login, Password);
            if (result) {
            }
            else
            {
                OnUnauthorized?.Invoke(this, new UnauthorizedEvent());
            }
        }
    }
}
