using Com.Appsflyer;
using Android.Util;

namespace com.appsflyer.xamarinsample
{
    public class MyPurchaseValidationCallback : Java.Lang.Object, IAppsFlyerInAppPurchaseValidationCallback
    {
        private const string TAG = "AppsFlyer_PurchaseCallback";
        private readonly MainActivity activity;

        public MyPurchaseValidationCallback(MainActivity mainActivity)
        {
            activity = mainActivity;
        }

        public void OnInAppPurchaseValidationError(IDictionary<string, Java.Lang.Object>? validationError)
        {
            string message = "OnInAppPurchaseValidationError:";
            if (validationError != null)
            {
                foreach (var kvp in validationError)
                {
                    message += $"\n  {kvp.Key}: {kvp.Value}";
                }
            }
            else
            {
                message += "\nError data is null";
            }
            Log.Error(TAG, message);
            Log.Error(TAG, message + "gttt");
            activity.UpdateStatusFromListener(message);
            activity.ShowToastOnListenerCallback("Purchase validation error.");
        }

        public void OnInAppPurchaseValidationFinished(IDictionary<string, Java.Lang.Object>? validationResult)
        {
            string message = "OnInAppPurchaseValidationFinished:";
            if (validationResult != null)
            {
                foreach (var kvp in validationResult)
                {
                    message += $"\n  {kvp.Key}: {kvp.Value}";
                }
            }
            else
            {
                message += "\nResult data is null";
            }
            Log.Info(TAG, message);
            activity.UpdateStatusFromListener(message);
            activity.ShowToastOnListenerCallback("Purchase validation finished.");
        }
    }
} 