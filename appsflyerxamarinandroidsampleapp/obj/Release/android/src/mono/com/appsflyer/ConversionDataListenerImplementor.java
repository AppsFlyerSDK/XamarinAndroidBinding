package mono.com.appsflyer;


public class ConversionDataListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.appsflyer.ConversionDataListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onConversionDataLoaded:(Ljava/util/Map;)V:GetOnConversionDataLoaded_Ljava_util_Map_Handler:Com.Appsflyer.IConversionDataListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"n_onConversionFailure:(Ljava/lang/String;)V:GetOnConversionFailure_Ljava_lang_String_Handler:Com.Appsflyer.IConversionDataListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"";
		mono.android.Runtime.register ("Com.Appsflyer.IConversionDataListenerImplementor, AppsFlyerXamarinBindingAndroid", ConversionDataListenerImplementor.class, __md_methods);
	}


	public ConversionDataListenerImplementor ()
	{
		super ();
		if (getClass () == ConversionDataListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Appsflyer.IConversionDataListenerImplementor, AppsFlyerXamarinBindingAndroid", "", this, new java.lang.Object[] {  });
	}


	public void onConversionDataLoaded (java.util.Map p0)
	{
		n_onConversionDataLoaded (p0);
	}

	private native void n_onConversionDataLoaded (java.util.Map p0);


	public void onConversionFailure (java.lang.String p0)
	{
		n_onConversionFailure (p0);
	}

	private native void n_onConversionFailure (java.lang.String p0);

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
