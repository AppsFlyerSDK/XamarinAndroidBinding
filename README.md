<img src="https://www.appsflyer.com/wp-content/uploads/2016/11/logo-1.svg"  width="200">


# Xamarin Android Binding


Xamarin Binding integration guide For Android
AppsFlyer Xamarin Binding version `1.50.0`
Built with AppsFlyer Android SDK `v4.8.18`



# 1. Overview

AppsFlyer SDK provides app installation and event tracking functionality. We have developed an SDK that is highly robust (7+ billion SDK installations to date), secure, lightweight and very simple to embed.



You can track installs, updates and sessions and also track additional in-app events beyond app installs (including in-app purchases, game levels, etc.) to evaluate ROI and user engagement levels.

---
AppsFlyer Xamarin binding provides application installation and events tracking functionality.

The API for the binding coincides with the native Android API, which can be found [here](https://support.appsflyer.com/hc/en-us/articles/207032126-AppsFlyer-SDK-Integration-Android).





## Table of content

- [Nuget](#nuget_install)
- [Quick Start](#quickStart)
- [API Methods](#api-methods)
    -  [SDK Initialization](#sdk_init)
    -  [Tracking In-App Events](#adding_events)
    -  [Get Conversion Data](#conversion_data)
    -  [Tracking Deep Linking](#deep_linking)
    - [Track App Uninstalls](#uninstall_tracking)
    - [Set Customer User ID](#SetCustomerUserId)
    - [Set Debug Log](#SetDebugLog)
    - [Get AppsFlyer UID](#GetAppsFlyerUID)
    - [Set Min Time Between Sessions](#SetMinTimeBetweenSessions)
    - [Set Device Tracking Disabled](#SetDeviceTrackingDisabled)
    -  [StopTracking](#StopTracking)
    -  [Wait For Customer User ID](#WaitForCustomerUserId)
    - [SetPreinstallAttribution](#SetPreinstallAttribution)
- [Sample App](#sample_app)



### <a id="nuget_install">


# Nuget

Install-Package AppsFlyerXamarinBindingAndroid



https://www.nuget.org/packages/AppsFlyerXamarinBindingAndroid/





### <a id="quickStart">



# 2.0 Quick Start



#### 2.1) Adding the Plugin to your Project

    1. Go to Project > Add NuGet Packages...
    2. Select the `AppsFlyerXamarinBindingAndroid`
    3. Select under version -  `1.4.0`
    4. Click `Add Package`



--------

To Embed SDK into your Application Manually:

    1. Copy AppsFlyerXamarinBindingAndroid.dll into your project.
    2. On Xamarin Studio go to References and click on Edit References.
    3. Go to .Net Assembly tab and click on Browse… button
    4. Locate AppsFlyerXamarinBindingAndroid.dll and chose it.
    5. Locate GooglePlayServicesLib.dll and add it as well (for advertising Id)


#### 2.2)  Setting the Required Permissions

The AndroidManifest.xml should include the following permissions:

```xml
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<!-- Optional : -->
<uses-permission android:name="android.permission.READ_PHONE_STATE" />
```


#### 2.3) Setting the BroadcastReceiver in AndroidManifest.xml

*_The following two options are available for implementing the install referrer broadcast receiver:_*

Using a Single Broadcast Receiver
If you do not have a receiver listening on the INSTALL_REFERRER, in the AndroidManifest.xml, add the following receiver within the application tag:

```xml
<receiver android:name="com.appsflyer.SingleInstallBroadcastReceiver" android:exported="true">
<intent-filter>
<action android:name="com.android.vending.INSTALL_REFERRER" />
</intent-filter>
</receiver>
```

Using a Multiple Broadcast Receiver

If you already have a receiver listening on the INSTALL_REFERRER, AppsFlyer provides a solution that broadcasts INSTALL_REFERRER to all other receivers automatically. In the AndroidManifest.xml, add the following receiver as the FIRST receiver for INSTALL_REFERRER, and ensure the receiver tag is within the application tag:

```xml
<receiver android:name="com.appsflyer.MultipleInstallBroadcastReceiver" android:exported="true">
<intent-filter>
<action android:name="com.android.vending.INSTALL_REFERRER" />
</intent-filter>
</receiver>
```

### <a id="api-methods">



# 3.0 API Methods



### <a id="sdk_init">


##  SDK Initialization



Go to your MainActivity.cs and add:



1) `using Com.Appsflyer;` at the top of the file.



2) Add the following code to the OnCreate() method:

```c#
AppsFlyerLib.Instance.StartTracking(this.Application, "YOUR_DEV_KEY");

/* AppsFlyerLib.Instance.SetDebugLog(true); */
```



### <a id="adding_events">

## Tracking In-App Events

Tracking in-app events is performed by calling `TrackEvent` with event name and value parameters. See [In-App Events](https://support.appsflyer.com/hc/en-us/articles/115005544169-AppsFlyer-Rich-In-App-Events-Android-and-iOS) documentation for more details.

Purchase Event Example:
```c#
Dictionary<string, Java.Lang.Object> eventValues = new  Dictionary<string, Java.Lang.Object>();  
eventValues.Add(AFInAppEventParameterName.Price , 2);  
eventValues.Add(AFInAppEventParameterName.Currency, "USD");  
eventValues.Add(AFInAppEventParameterName.Quantity, "1");  
AppsFlyerLib.Instance.TrackEvent(this.BaseContext, AFInAppEventType.Purchase , eventValues);
```



### <a id="conversion_data">

##  Get Conversion Data



For Conversion data your should call this method:


```c#
AppsFlyerLib.RegisterConversionListener (this, new AppsFlyerConversionDelegate ());
```



To access AppsFlyer's conversion data from the Android SDK implement the ConversionDataListener:

```c#
public class AppsFlyerConversionDelegate : Java.Lang.Object, IAppsFlyerConversionListener {

public AppsFlyerConversionDelegate()
{
Console.WriteLine("AppsFlyerConversionDelegate called");
}
public void OnAppOpenAttribution(IDictionary<string, string> p0)
{
Console.WriteLine("OnAppOpenAttribution = " + p0.ToString());
}
public void OnAttributionFailure(string p0)
{
Console.WriteLine("OnAttributionFailure = " + p0);
}
public void OnInstallConversionDataLoaded(IDictionary<string, string> p0)
{
foreach (var kvp in p0){
Console.WriteLine(kvp.Key + " = " + kvp.Value);
}
}
public void OnInstallConversionFailure(string p0)
{
Console.WriteLine("OnInstallConversionFailure = " + p0);
}
}

```

AppsFlyerConversionDelegate.cs can be found in the sample app provided with this guide.  


### <a id="deep_linking">

## Tracking Deep Linking

1. Add relevant intent filters to the activity.

    a. You can read more about intent filters for deep linking here: https://developer.android.com/training/app-links/deep-linking.html
    
    b. You can read more on implementing Android Manifest by adding custom attributed in Xamarin here: https://docs.microsoft.com/en-us/xamarin/android/platform/android-manifest
    
Example intent filter attribute implementation:
```c#
[Activity(Label = "appsflyerxamarinandroidsampleapp", MainLauncher = true, Icon = "@mipmap/icon")]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
                   AutoVerify = true,
                   Categories = new[]
                   {
                        Android.Content.Intent.CategoryDefault,
                        Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "https",
                   DataHost = "domain.com",
                   DataPathPrefix = "/path/")]
public class MainActivity : Activity
{...}
```

2. Add the following code in the Activity onCreate method:

```c#
AppsFlyerLib.Instance.SendDeepLinkData(this);
```

More information on Deep Linking on Android can be found here: 

https://support.appsflyer.com/hc/en-us/articles/207032126-AppsFlyer-SDK-Integration-Android#5-tracking-deep-linking



### <a id="uninstall_tracking">

## Uninstall tracking

AppsFlyer enables you to track app uninstalls.

To complete this process fully and correctly, you must [read here](https://support.appsflyer.com/hc/en-us/articles/208004986).



1. Add the Xamarin Google Play Services - GCM Nugget to your project.

2. Add the following permissions to your AndroidManifest.xml file:

```xml
<uses-permission android:name="android.permission.WAKE_LOCK" />
<permission android:name="your.app.name.permission.C2D_MESSAGE"
android:protectionLevel="signature" />
<uses-permission android:name="your.app.name.permission.C2D_MESSAGE" />
<uses-permission android:name="com.google.android.c2dm.permission.RECEIVE" />

```

3. Add the following receiver to your AndroidManifest.xml file:

```xml
<receiver
android:name="com.google.android.gms.gcm.GcmReceiver"
android:exported="true">
<intent-filter>
<action android:name="com.google.android.c2dm.intent.RECEIVE" />
</intent-filter>
</receiver>
```

5. Add the following method call before "startTracking":


```c#
AppsFlyerLib.Instance.SetGCMProjectNumber(this.Application, "SenderID");
```

5. Add your Server Key to AppsFlyer's dashboard.



For more information regarding how to obtain the Server Key and SenderID, please review this article : https://support.appsflyer.com/hc/en-us/articles/208004986-Android-Uninstall-Tracking



### <a id="SetCustomerUserId">

## Set Customer User ID

```c#
AppsFlyerLib.Instance.SetCustomerUserId("custom_id");
```

### <a id="SetDebugLog">

## Set Debug Log

```c#
AppsFlyerLib.Instance.SetDebugLog(true);
```

### <a id="GetAppsFlyerUID">

## Get AppsFlyer UID

```c#
AppsFlyerLib.Instance.GetAppsFlyerUID(this.BaseContext);
```

### <a id="SetMinTimeBetweenSessions">

## Set Min Time Between Sessions
For AppsFlyer to count two separate sessions, the default time between each session must be at least 5 seconds. A session commences when the user opens the app. If you want to configure a different time between sessions, use the following API: 

```c#
AppsFlyerLib.Instance.SetMinTimeBetweenSessions(int time);
```

### <a id="SetDeviceTrackingDisabled">

## Set Device Tracking Disabled

```c#
AppsFlyerLib.Instance.SetDeviceTrackingDisabled(true);
```

### <a id="StopTracking">

## Stop Tracking
In some extreme cases you might want to shut down all SDK tracking due to legal and privacy compliance. This can be achieved with the isStopTracking API. Once this API is invoked, our SDK will no longer communicate with our servers and stop functioning.

```c#
AppsFlyerLib.Instance.StopTracking(true, this.BaseContext);
```

### <a id="WaitForCustomerUserId">

## Wait For Customer User ID
It is possible to delay the SDK Initialization until the customerUserID is set. This feature makes sure that the SDK doesn't begin functioning until the customerUserID is provided. If this API is used, all in-app events and any other SDK API calls are discarded, until the customerUserID is provided and tracked.

```c#
AppsFlyerLib.Instance.WaitForCustomerUserId(true);
AppsFlyerLib.Instance.SetCustomerIdAndTrack("custom_id", this.BaseContext);
```
### <a id="SetPreinstallAttribution">

## Set Preinstall Attribution

```c#
AppsFlyerLib.Instance.SetPreinstallAttribution(string mediaSource, string campaign, string siteId);
```


### <a id="sample_app">
## Sample App 
Sample app can be found here:
https://github.com/AppsFlyerSDK/XamarinAndroidBinding/tree/master/appsflyerxamarinandroidsampleapp  


---

In order for us to provide optimal support, we would kindly ask you to submit any issues to support@appsflyer.com.



*_When submitting an issue please specify your AppsFlyer sign-up (account) email, your app ID, production steps, logs, code snippets and any additional relevant information._*





