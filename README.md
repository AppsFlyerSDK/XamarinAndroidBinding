# XamarinAndroidBinding

Xamarin Binding integration guide For Android
AppsFlyer Xamarin Binding version 1.0.0

1.	Introduction
AppsFlyer’s Xamarin binding provides application installation and events tracking functionality


2.   Initial steps

To Embed SDK into your Application:
2.1 Copy AppsFlyerXamarinBindingAndroid.dll into your project.
2.2 On Xamarin Studio go to References and click on Edit References. 2.3 Go to .Net Assembly tab and click on Browse… button
2.4 Locate AppsFlyerXamarinBindingAndroid.dll and chose it.
2.5 Locate GooglePlayServicesLib.dll and add it as well (for advertising Id



3.  SDK Initialization
Go to your MainActivity.cs and add:
using Com.Appsflyer; at the top of the file.

Add the following code in the OnCreate method:

protected override void OnCreate (Bundle bundle)

AppsFlyerLib.SetAppsFlyerKey ("YOUR_DEV_KEY_HERE"); 
AppsFlyerLib.SendTracking (this);

Basically, every API call for Android SDK is available here as well. For more information please refer to Appsflyer Android Integration guide.


3.1 Set your appId & DevKey 
Replace devKey with your values
You can get your AppsFlyer DevKey on our dashboard. See “SDK integration” on your app screen. 

DevKey = your unique developer ID, which is accessible from your account, e.g. rbz2mfgZQY5mSEYNTyjwni // For example: 

4.	Adding Custom Event 
Example: “Add-to-cart” Event 
AppsFlyerLib.TrackEvent(this, AFInAppEventType.AddToCart, new Dictionary<string, Java.Lang.Object> { 
{AFInAppEventParameterName.ContentId, "id1234"}, 
{AFInAppEventParameterName.ContentType, "tickets"},
{AFInAppEventParameterName.Price, 123},
{AFInAppEventParameterName.Currency, "USD"},
});



5.	Conversion Data
For Conversion data your should call this method:
AppsFlyerLib.RegisterConversionListener (this, new AppsFlyerConversionDelegate ()); 
AppsFlyerConversionDelegate.cs can be found in the sample app provided with this guide (see screenshot below)
