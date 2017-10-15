using MvvmCross.Core.ViewModels;
using ParentControl.Mobile.Core.Services.Contracts;

namespace ParentControl.Mobile.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private MvxCommand _loginCommand;

        private IAuthorizationService _authorizationService;

        public LoginViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public MvxCommand LoginCommand
        {
            get
            {
                _loginCommand = _loginCommand ?? new MvxCommand(LoginAction);
                return _loginCommand;
            }
        }

        public void LoginAction()
        {
            _authorizationService.Login(Login, Password);
        }
    }
}
