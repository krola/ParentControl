package md5fbb3dbc4d574a65e88e6241f2f202e22;


public class SplashScreenView
	extends mvvmcross.droid.views.MvxSplashScreenActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ParentControl.Mobile.Android.View.SplashScreenView, ParentControl.Mobile.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SplashScreenView.class, __md_methods);
	}


	public SplashScreenView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SplashScreenView.class)
			mono.android.TypeManager.Activate ("ParentControl.Mobile.Android.View.SplashScreenView, ParentControl.Mobile.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
