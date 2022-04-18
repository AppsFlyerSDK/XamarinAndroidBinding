using System;
using Android.App;
using Com.Appsflyer;
using Firebase.Messaging;


[Service(Exported = true)]
[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
public class MyNewFirebaseManager : FirebaseMessagingService
{
    public override void OnNewToken(string newToken)
    {
        base.OnNewToken(newToken);
        // Sending new token to AppsFlyer
        Console.WriteLine("MyNewFirebaseManager onNewToken");
        AppsFlyerLib.Instance.UpdateServerUninstallToken(ApplicationContext, newToken);
        // the rest of the code that makes use of the token goes in this method as well
    }
}
