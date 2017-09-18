package md5fbb3dbc4d574a65e88e6241f2f202e22;


public class LoginView
	extends mvvmcross.droid.views.MvxActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ParentControl.Mobile.Android.View.LoginView, ParentControl.Mobile.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginView.class, __md_methods);
	}


	public LoginView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoginView.class)
			mono.android.TypeManager.Activate ("ParentControl.Mobile.Android.View.LoginView, ParentControl.Mobile.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
