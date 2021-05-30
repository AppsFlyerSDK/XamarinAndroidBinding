using System;
using Com.Appsflyer.Deeplink;

namespace XamarinSample
{
    public class DeepLinkListener : Java.Lang.Object, IDeepLinkListener
    {
        private MainActivity activity;

        public DeepLinkListener(MainActivity activity) 
        {
            this.activity = activity;
        }

        public void OnDeepLinking(DeepLinkResult deepLinkResult)
        {
            string message = "OnDeepLinking:\n";
            message += deepLinkResult.ToString();
            message = message + "\nTimestamp: " + DateTime.Now;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                activity.udlTextView.Text = message;
            });
        }
    }
}
