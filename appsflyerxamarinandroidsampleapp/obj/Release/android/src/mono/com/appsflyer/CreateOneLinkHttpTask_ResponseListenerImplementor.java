package mono.com.appsflyer;


public class CreateOneLinkHttpTask_ResponseListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.appsflyer.CreateOneLinkHttpTask.ResponseListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onResponse:(Ljava/lang/String;)V:GetOnResponse_Ljava_lang_String_Handler:Com.Appsflyer.CreateOneLinkHttpTask/IResponseListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"n_onResponseError:(Ljava/lang/String;)V:GetOnResponseError_Ljava_lang_String_Handler:Com.Appsflyer.CreateOneLinkHttpTask/IResponseListenerInvoker, AppsFlyerXamarinBindingAndroid\n" +
			"";
		mono.android.Runtime.register ("Com.Appsflyer.CreateOneLinkHttpTask+IResponseListenerImplementor, AppsFlyerXamarinBindingAndroid", CreateOneLinkHttpTask_ResponseListenerImplementor.class, __md_methods);
	}


	public CreateOneLinkHttpTask_ResponseListenerImplementor ()
	{
		super ();
		if (getClass () == CreateOneLinkHttpTask_ResponseListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Appsflyer.CreateOneLinkHttpTask+IResponseListenerImplementor, AppsFlyerXamarinBindingAndroid", "", this, new java.lang.Object[] {  });
	}


	public void onResponse (java.lang.String p0)
	{
		n_onResponse (p0);
	}

	private native void n_onResponse (java.lang.String p0);


	public void onResponseError (java.lang.String p0)
	{
		n_onResponseError (p0);
	}

	private native void n_onResponseError (java.lang.String p0);

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
