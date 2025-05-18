using Com.Appsflyer;
using Firebase.Messaging;
using Android.Util; 

namespace Sample.Android 
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyNewFirebaseManager : FirebaseMessagingService
    {
        private const string TAG = "MyFirebaseManager";

        public override void OnNewToken(string newToken)
        {
            base.OnNewToken(newToken);
            Log.Info(TAG, $"Firebase OnNewToken: {newToken}");
            // Sending new token to AppsFlyer for uninstall measurement
            AppsFlyerLib.Instance.UpdateServerUninstallToken(this.ApplicationContext, newToken);
            Log.Info(TAG, "Sent new FCM token to AppsFlyer.");
            // You can add other logic here if your app uses FCM for other purposes.
        }
    }
} 