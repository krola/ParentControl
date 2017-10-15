using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace ParentControl.Mobile.Core.ViewModels
{
    public class SplashScreenViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public SplashScreenViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ConnectToService();
        }

        private void ConnectToService()
        {
            Task.Delay(5000);
            _navigationService.Navigate(new LoginViewModel(null));
        }
    }
}
