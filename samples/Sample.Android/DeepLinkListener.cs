using Com.Appsflyer.Deeplink;
using Android.Util;

namespace com.appsflyer.xamarinsample
{
    public class DeepLinkListener : Java.Lang.Object, IDeepLinkListener
    {
        private MainActivity activity;
        private const string TAG = "AppsFlyer_DeepLinkListener";

        public DeepLinkListener(MainActivity mainActivity) 
        {
            activity = mainActivity;
        }

        public void OnDeepLinking(DeepLinkResult deepLinkResult)
        {
            string message = "OnDeepLinking:\n";
            message += deepLinkResult.ToString();
            message = message + "\nTimestamp: " + DateTime.Now;
            Console.WriteLine(message);
            Log.Debug(TAG, message);
            activity.RunOnUiThread(() =>
            {
                activity.UpdateStatusFromListener(message);
            });
        }
    }
} 