using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace ParentControl.Setup.Droid
{
    [Activity(
        Label = "ParentControl.Setup"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
