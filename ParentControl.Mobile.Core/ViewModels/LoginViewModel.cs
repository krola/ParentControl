using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ParentControl.Core.Contracts.Services;

namespace ParentControl.Mobile.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private MvxCommand _loginCommand;
        private IHttpService _httpService;

        public LoginViewModel()
        {
            //_httpService = Mvx.Resolve<IHttpService>();
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
            
        }
    }
}
