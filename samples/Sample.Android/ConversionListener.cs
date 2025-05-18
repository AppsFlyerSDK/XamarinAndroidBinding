using Android.Util;
using Com.Appsflyer;

namespace com.appsflyer.xamarinsample
{
    public class ConversionListener : Java.Lang.Object, IAppsFlyerConversionListener
    {
        private MainActivity activity;
        private const string TAG = "AppsFlyer_ConvListener";

        public ConversionListener(MainActivity mainActivity)
        {
            activity = mainActivity;
        }

        public void OnAppOpenAttribution(IDictionary<string, string>? attributionData) 
        {
            string message = "OnAppOpenAttribution:";
            if (attributionData != null)
            {
                foreach (var kvp in attributionData)
                {
                    message += $"\n{kvp.Key}: {kvp.Value}";
                }
            }
            else
            {
                message += "\nData is null";
            }
            Log.Info(TAG, message);
            activity.UpdateStatusFromListener(message);
        }

        public void OnAttributionFailure(string? errorMessage) 
        {
            string message = $"OnAttributionFailure: {errorMessage ?? "Unknown error"}";
            Log.Error(TAG, message);
            activity.UpdateStatusFromListener(message);
        }

        public void OnConversionDataFail(string? errorMessage) 
        {
            string message = $"OnConversionDataFail: {errorMessage ?? "Unknown error"}";
            Log.Error(TAG, message);
            activity.UpdateStatusFromListener(message);
            activity.ShowToastOnListenerCallback("Conversion data failed.");
        }

        public void OnConversionDataSuccess(IDictionary<string, Java.Lang.Object>? conversionData) 
        {
            string message = "OnConversionDataSuccess:";
            if (conversionData != null)
            {
                foreach (var kvp in conversionData)
                {
                    message += $"\n{kvp.Key}: {kvp.Value}";
                }
            }
            else
            {
                message += "\nData is null";
            }
            message += $"\nTimestamp: {DateTime.Now}";
            Log.Info(TAG, message);
            activity.UpdateStatusFromListener(message);
            activity.ShowToastOnListenerCallback("Conversion data received.");
        }
    }
} 