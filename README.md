# XamarinAndroidBinding

Xamarin Binding integration guide For Android
AppsFlyer Xamarin Binding version 1.4.0
Built with AppsFlyer Android SDK v4.8.11

#Introduction
AppsFlyer’s Xamarin binding provides application installation and events tracking functionality


# Nuget
Install-Package AppsFlyerXamarinBindingAndroid<br>
https://www.nuget.org/packages/AppsFlyerXamarinBindingAndroid/


# Initial steps

To Embed SDK into your Application:

1. Copy AppsFlyerXamarinBindingAndroid.dll into your project.

2. On Xamarin Studio go to References and click on Edit References. 

3. Go to .Net Assembly tab and click on Browse… button

4. Locate AppsFlyerXamarinBindingAndroid.dll and chose it.

5. Locate GooglePlayServicesLib.dll and add it as well (for advertising Id


# SDK Initialization

Go to your MainActivity.cs and add:

using Com.Appsflyer; at the top of the file.

Add the following code in the OnCreate method:

	AppsFlyerLib.Instance.StartTracking (this.Application, "YOUR_DEV_KEY");

Basically, every API call for Android SDK is available here as well. For more information please refer to Appsflyer Android Integration guide.


Set your appId & DevKey 
Replace devKey with your values
You can get your AppsFlyer DevKey on our dashboard. See “SDK integration” on your app screen. 

DevKey = your unique developer ID, which is accessible from your account, e.g. rbz2mfgZQY5mSEYNTyjwni // For example: 

# Adding Custom Event 
Example: “Add-to-cart” Event:

	AppsFlyerLib.TrackEvent(this, AFInAppEventType.AddToCart, new Dictionary<string, Java.Lang.Object> { 
	{AFInAppEventParameterName.ContentId, "id1234"}, 
	{AFInAppEventParameterName.ContentType, "tickets"},
	{AFInAppEventParameterName.Price, 123},
	{AFInAppEventParameterName.Currency, "USD"},
	});



# Conversion Data
For Conversion data your should call this method:

	AppsFlyerLib.RegisterConversionListener (this, new AppsFlyerConversionDelegate ()); 
AppsFlyerConversionDelegate.cs can be found in the sample app provided with this guide 


# Uninstall tracking

1. Add the Xamarin Google Play Services - GCM Nugget to your project.
2. Add the following permissions to your AndroidManifest.xml file:
```
<uses-permission android:name="android.permission.WAKE_LOCK" />
<permission android:name="your.app.name.permission.C2D_MESSAGE"
	android:protectionLevel="signature" />
<uses-permission android:name="your.app.name.permission.C2D_MESSAGE" />
<uses-permission android:name="com.google.android.c2dm.permission.RECEIVE" />
```
3. Add the following receiver to your AndroidManifest.xml file:
```
<receiver
   android:name="com.google.android.gms.gcm.GcmReceiver"
   android:exported="true">
   <intent-filter>
       <action android:name="com.google.android.c2dm.intent.RECEIVE" />
   </intent-filter>
</receiver>
```
3. Add the following method call before "startTracking":

```AppsFlyerLib.Instance.SetGCMProjectNumber(this.Application, "SenderID");```

4. Add your Server Key to AppsFlyer's dashboard.

For more information regarding how to obtain the Server Key and SenderID, please review this article : https://support.appsflyer.com/hc/en-us/articles/208004986-Android-Uninstall-Tracking

