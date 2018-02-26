using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using ParentControl.Mobile.Core;

namespace ParentControl.Mobile.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected void HandleUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            // log won't be available, because dalvik is destroying the process
            //Log.Debug (logTag, "MyHandler caught : " + e.Message);
            // instead, your err handling code shoudl be run:
            Console.WriteLine("========= MyHandler caught : " + e.Message);
        }
    }
}