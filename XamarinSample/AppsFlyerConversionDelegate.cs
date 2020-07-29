using System;
using Com.Appsflyer;
using System.Collections.Generic;
using Android.Support.V7.App;

namespace XamarinSample
{
    public class AppsFlyerConversionDelegate : Java.Lang.Object, IAppsFlyerConversionListener
    {
        AppCompatActivity activity;
        Android.Support.V7.Widget.AppCompatTextView oaoaTextView;
        Android.Support.V7.Widget.AppCompatTextView gcdTextView;

        public AppsFlyerConversionDelegate(MainActivity activity)
        {
            this.oaoaTextView = activity.oaoaTextView;
            this.gcdTextView = activity.gcdTextView;
            this.activity = activity;
            Console.WriteLine("AppsFlyerConversionDelegate called");
        }

        public void OnAppOpenAttribution(IDictionary<string, string> p0)
        {
            string message = "OnAppOpenAttribution:\n";
            foreach (var kvp in p0)
            {
                message = message + kvp.Key.ToString() + " = " + kvp.Value.ToString() + "\n";
            }
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                oaoaTextView.Text = message;
            });
        }

        public void OnAttributionFailure(string p0)
        {
            string message = "OnAttributionFailure = " + p0;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                oaoaTextView.Text = message;
            });
        }

        public void OnConversionDataFail(string p0)
        {
            string message = "OnInstallConversionFailure = " + p0;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                gcdTextView.Text = message;
            });
        }

        public void OnConversionDataSuccess(IDictionary<string, Java.Lang.Object> p0)
        {
            string message = "OnConversionDataSuccess:\n";
            foreach (var kvp in p0)
            {
                if (kvp.Value != null)
                {
                    message = message + kvp.Key.ToString() + " = " + kvp.Value.ToString() + "\n";
                }
            }
            message = message + "Timestamp:" + DateTime.Now;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                gcdTextView.Text = message;
            });
        }
    }
}
