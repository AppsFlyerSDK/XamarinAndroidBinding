﻿using Com.Appsflyer;
using Com.Appsflyer.Internal;

namespace Sample.NuGet.NET;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        AppsFlyerLib.Instance.SetDebugLog(true);
        //AppsFlyerLib.Instance.SetLogLevel(AFLogger.LogLevel.Verbose); // Enable verbose logs for debugging
        AppsFlyerLib.Instance.Init("DEV_KEY", null, Application);
        AppsFlyerLib.Instance.Start(this, "DEV_KEY");
        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);
    }
}
