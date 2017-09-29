using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform;
using ParentControl.Mobile.Core.ViewModels;

namespace ParentControl.Mobile.Core
{
    class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {

            //var auth = Mvx.Resolve<IAuth>();
            //if (auth.Check())
            //{
            //    ShowViewModel<HomeViewModel>();
            //}
            //else
            //{
                ShowViewModel<LoginViewModel>();
            //}
        }
    }
}
