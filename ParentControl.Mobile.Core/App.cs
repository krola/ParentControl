﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ParentControl.Core.Configuration;
using ParentControl.Core.Contracts;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Mobile.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IConfiguration, Configuration>();
            var config = Mvx.Resolve<IConfiguration>();
            Mvx.RegisterSingleton<IHttpService>(new HttpService(config));
            //var calcExample = Mvx.Resolve<IBillCalculator>();

            // Tells the MvvmCross framework that whenever any code requests an IMvxAppStart reference,
            // the framework should return that same appStart instance.
            var appStart = new AppStart();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);
        }
    }
}
