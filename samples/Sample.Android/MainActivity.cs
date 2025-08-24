using Com.Appsflyer;
using Android.Util;

namespace com.appsflyer.xamarinsample
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    [Android.Runtime.Register("com.appsflyer.xamarinsample.MainActivity")]
    public class MainActivity : Activity
    {
        public TextView? statusTextView;
        public TextView? sdkVersionTextView;
        private Button? logEventButton;
        private Button? logEventWithParamsButton;
        private Button? setCustomUserIdButton;
        private Button? getAppsFlyerIdButton;
        private Button? validatePurchaseButton;

        private const string TAG = "AppsFlyer_SampleApp";
        private string? devKey;

        // Declare listener instances
        private ConversionListener? conversionListener;
        private DeepLinkListener? deepLinkListener;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(global::Sample.Android.Resource.Layout.activity_main); 

            devKey = Resources.GetString(global::Sample.Android.Resource.String.appsflyer_dev_key);
            if (string.IsNullOrEmpty(devKey) || devKey == "YOUR_APPSFLYER_DEV_KEY")
            {
                Log.Error(TAG, "AppsFlyer Dev Key is not set or is a placeholder. Please set it in secrets.xml.");
                UpdateStatus("ERROR: AppsFlyer Dev Key not set!");
            }

            statusTextView = FindViewById<TextView>(global::Sample.Android.Resource.Id.statusTextView);
            sdkVersionTextView = FindViewById<TextView>(global::Sample.Android.Resource.Id.sdkVersionTextView);
            logEventButton = FindViewById<Button>(global::Sample.Android.Resource.Id.logEventButton);
            logEventWithParamsButton = FindViewById<Button>(global::Sample.Android.Resource.Id.logEventWithParamsButton); 
            setCustomUserIdButton = FindViewById<Button>(global::Sample.Android.Resource.Id.setCustomUserIdButton);
            getAppsFlyerIdButton = FindViewById<Button>(global::Sample.Android.Resource.Id.getAppsFlyerIdButton);
            validatePurchaseButton = FindViewById<Button>(global::Sample.Android.Resource.Id.validatePurchaseButton);

            UpdateStatus("Activity Created. SDK not initialized yet.");

            // Instantiate listeners
            conversionListener = new ConversionListener(this);
            deepLinkListener = new DeepLinkListener(this);

            // Initialize AppsFlyer SDK
            AppsFlyerLib appsFlyerLib = AppsFlyerLib.Instance;
            appsFlyerLib.SetDebugLog(true);
            appsFlyerLib.Init(devKey, conversionListener, this.ApplicationContext); 
            appsFlyerLib.SubscribeForDeepLink(deepLinkListener); 
            appsFlyerLib.Start(this);

            // Get and display SDK version
            if (sdkVersionTextView != null)
            {
                string sdkVersion = appsFlyerLib.SdkVersion;
                sdkVersionTextView.Text = $"SDK Version: {sdkVersion}";
            }

            UpdateStatus("AppsFlyer Init, SubscribeForDeepLink, and Start called in OnCreate.");
            Log.Info(TAG, "AppsFlyerLib.Instance.Init, SubscribeForDeepLink and Start called.");
            

            logEventButton.Click += (sender, e) => 
            {
                string eventName = "af_simple_event";
                UpdateStatus($"Logging event: {eventName}");
                AppsFlyerLib.Instance.LogEvent(this, eventName, null);
                Log.Info(TAG, $"Logged event: {eventName}");
                ShowToast($"Event '{eventName}' logged.");
            };
            

        
            logEventWithParamsButton.Click += (sender, e) => 
            {
                string eventName = "af_event_with_params";

                var eventValues = new Dictionary<string, Java.Lang.Object>
                {
                    { "app_version", new Java.Lang.String("1.0.0") },
                    { "event_source", new Java.Lang.String("button_click") },
                    { "item_id", new Java.Lang.String("123") }
                };
                UpdateStatus($"Logging event: {eventName} with parameters");
                AppsFlyerLib.Instance.LogEvent(this, eventName, eventValues);
                Log.Info(TAG, $"Logged event: {eventName} with params.");
                ShowToast($"Event '{eventName}' with params logged.");
            };
        

        
            setCustomUserIdButton.Click += (sender, e) => 
            {
                string cuid = "test-cuid-12345";
                UpdateStatus($"Setting Custom User ID: {cuid}");
                AppsFlyerLib.Instance.SetCustomerUserId(cuid);
                Log.Info(TAG, $"Set Custom User ID: {cuid}");
                ShowToast($"Custom User ID '{cuid}' set.");
            };
            

            
            getAppsFlyerIdButton.Click += (sender, e) =>
            {
                string? appsFlyerId = AppsFlyerLib.Instance.GetAppsFlyerUID(this); // Can be null
                UpdateStatus($"AppsFlyer ID: {appsFlyerId ?? "Not available"}");
                Log.Info(TAG, $"Retrieved AppsFlyer ID: {appsFlyerId ?? "Not available"}");
                ShowToast($"AppsFlyer ID: {appsFlyerId ?? "Not available"}");
            };
            

            
            validatePurchaseButton.Click += (sender, e) =>
            {
                try
                {
                    ShowToast("Called ValidateAndLogInAppPurchase.");
                    Log.Info(TAG, "Called ValidateAndLogInAppPurchase");
                    ValidateAndLogV2Example();
                }
                catch (System.Exception ex)
                {
                    Log.Error(TAG, $"Error calling ValidateAndLogInAppPurchase: {ex.Message}");
                    ShowToast("Error calling purchase validation");
                }
            };
            
        }

        // Public method for listeners to update status
        public void UpdateStatusFromListener(string message)
        {
            RunOnUiThread(() => 
            {
                if (statusTextView != null) 
                {
                    statusTextView.Text = $"Listener: {message}";
                }
            });
        }

        // Public method for listeners to show toast
        public void ShowToastOnListenerCallback(string message)
        {
            RunOnUiThread(() => Toast.MakeText(this, message, ToastLength.Short).Show());
        }

        private void UpdateStatus(string message)
        {
            RunOnUiThread(() => 
            {
                if (statusTextView != null) 
                {
                    statusTextView.Text = $"Status: {message}";
                }
            });
            Log.Debug(TAG, message); // Also log to Logcat
        }

        private void ShowToast(string message)
        {
            RunOnUiThread(() => Toast.MakeText(this, message, ToastLength.Short).Show());
        }

        protected override void OnNewIntent(global::Android.Content.Intent? intent) // Made intent nullable
        {
            base.OnNewIntent(intent);
            this.Intent = intent; // Correct way to set the activity's intent
            Log.Info(TAG, "OnNewIntent called.");
            if (intent != null)
            {
                 AppsFlyerLib.Instance.PerformOnDeepLinking(intent, this); // Recommended for UDL if activity is re-launched
                 UpdateStatus("OnNewIntent: Called PerformOnDeepLinking.");
            }
            else
            {
                UpdateStatus("OnNewIntent: Intent was null.");
            }
        }

        private void ValidateAndLogV2Example()
        {
            //AFPurchaseDetails
            // An object that encapsulates all data related to the purchase provided to the validateAndLogInAppPurchase method.
            // Note: As per v6.17.3, the constructor now only requires 3 parameters (purchaseType, purchaseToken, productId)
            AFPurchaseDetails purchaseDetails = new AFPurchaseDetails(
                AFPurchaseType.OneTimePurchase,
                "purchase_token",
                "productId"
                );

            // Additional purchase details (previously called extra_event_values, now purchase_additional_details)
            Dictionary<string, string> customParameters = new Dictionary<string, string>
                {
                    { "Country", "US" },
                    { "myCustomParam", "32423bwdfnsdf"}
                };

            var validationCallback = new MyPurchaseValidationCallback(this);

            AppsFlyerLib.Instance.ValidateAndLogInAppPurchase(purchaseDetails, customParameters, validationCallback);
            
            UpdateStatus("Purchase validation requested - check logs for callback results");
        }
    }
} 