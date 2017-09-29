using MvvmCross.Core.ViewModels;

namespace ParentControl.Mobile.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private MvxCommand _loginCommand;

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
