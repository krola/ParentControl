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
using MvvmCross.Droid.Views;

namespace ParentControl.Mobile.Android.View
{
    [Activity(Label = "LoginView", NoHistory = true)]
    public class LoginView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            // Create your application here
        }

        protected override void OnPause()
        {
            ViewModel.OnUnauthorized -= ViewModel_OnUnauthorized;
            base.OnPause();
        }

        protected override void OnResume()
        {
            ViewModel.OnUnauthorized -= ViewModel_OnUnauthorized;
            base.OnResume();
        }

        private void ViewModel_OnUnauthorized(object sender, GenericWebViewViewModel.UnauthorizedEvent e)
        {
            Toast.MakeText(this, $"Selected Monkey {e.Monkey.Name }", ToastLength.Short)
                .Show();
        }
    }
}