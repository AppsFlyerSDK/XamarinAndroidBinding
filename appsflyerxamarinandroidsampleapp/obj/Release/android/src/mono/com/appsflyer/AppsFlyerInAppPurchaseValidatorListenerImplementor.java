package mono.com.appsflyer;


public class AppsFlyerInAppPurchaseValidatorListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.appsflyer.AppsFlyerInAppPurchaseValidatorListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onValidateInApp:()V:GetOnValidateInAppHandler:Com.Appsflyer.IAppsFlyerInAppPurchaseValidatorListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"n_onValidateInAppFailure:(Ljava/lang/String;)V:GetOnValidateInAppFailure_Ljava_lang_String_Handler:Com.Appsflyer.IAppsFlyerInAppPurchaseValidatorListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"";
		mono.android.Runtime.register ("Com.Appsflyer.IAppsFlyerInAppPurchaseValidatorListenerImplementor, AppsFlyerXamarinBindingAndroid", AppsFlyerInAppPurchaseValidatorListenerImplementor.class, __md_methods);
	}


	public AppsFlyerInAppPurchaseValidatorListenerImplementor ()
	{
		super ();
		if (getClass () == AppsFlyerInAppPurchaseValidatorListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Appsflyer.IAppsFlyerInAppPurchaseValidatorListenerImplementor, AppsFlyerXamarinBindingAndroid", "", this, new java.lang.Object[] {  });
	}


	public void onValidateInApp ()
	{
		n_onValidateInApp ();
	}

	private native void n_onValidateInApp ();


	public void onValidateInAppFailure (java.lang.String p0)
	{
		n_onValidateInAppFailure (p0);
	}

	private native void n_onValidateInAppFailure (java.lang.String p0);

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
