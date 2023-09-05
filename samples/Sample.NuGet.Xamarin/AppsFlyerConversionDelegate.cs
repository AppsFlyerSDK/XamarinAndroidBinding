using System;
using Com.Appsflyer;
using System.Collections.Generic;

namespace XamarinSample
{
    public class ConversionListener : Java.Lang.Object, IAppsFlyerConversionListener
    {
        MainActivity activity;

        public ConversionListener(MainActivity activity)
        {
            this.activity = activity;
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
                activity.udlTextView.Text = message;
            });
        }

        public void OnAttributionFailure(string p0)
        {
            string message = "OnAttributionFailure = " + p0;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                activity.udlTextView.Text = message;
            });
        }

        public void OnConversionDataFail(string p0)
        {
            string message = "OnInstallConversionFailure = " + p0;
            Console.WriteLine(message);
            activity.RunOnUiThread(() =>
            {
                activity.gcdTextView.Text = message;
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
            activity.RunOnUiThread(() =>
            {
                Console.WriteLine(message);
                activity.gcdTextView.Text = message;
            });
        }
    }
}
