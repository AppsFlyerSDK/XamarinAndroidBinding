using Com.Appsflyer;

namespace Sample.NuGet.NET;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        AppsFlyerLib.Instance.SetDebugLog(true);
        //AppsFlyerLib.Instance.SetLogLevel(AFLogger.LogLevel.Verbose); // Enable verbose logs for debugging
        AppsFlyerLib.Instance.Init("4UGrDF4vFvPLbHq5bXtCza", null, Application);
        AppsFlyerLib.Instance.Start(this, "4UGrDF4vFvPLbHq5bXtCza");
        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);
    }
}
