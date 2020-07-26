using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Appsflyer;

namespace XamarinSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
                   AutoVerify = true,
                   Categories = new[]
                   {
                        Android.Content.Intent.CategoryDefault,
                        Android.Content.Intent.CategoryBrowsable
                   },
                   DataScheme = "sdktest")]
    public class MainActivity : AppCompatActivity
    {
        public AppCompatTextView gcdTextView;
        public AppCompatTextView oaoaTextView;
        public FloatingActionButton purchaseButton;
        FloatingActionButton fab;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            purchaseButton = FindViewById<FloatingActionButton>(Resource.Id.purchase_button);
            purchaseButton.Click += PurchaseButtonClick;

            gcdTextView = FindViewById<AppCompatTextView>(Resource.Id.gcd_text_view);
            oaoaTextView = FindViewById<AppCompatTextView>(Resource.Id.oaoa_text_view);

            AppsFlyerLib.Instance.SetDebugLog(true);
            AppsFlyerLib.Instance.SetLogLevel(AFLogger.LogLevel.Verbose);
            AppsFlyerLib.Instance.Init("4UGrDF4vFvPLbHq5bXtCza", new AppsFlyerConversionDelegate(this), Application);
            AppsFlyerLib.Instance.SetAppInviteOneLink("E2bM"); // Replace with OneLink ID from your AppsFlyer account
            AppsFlyerLib.Instance.SetSharingFilter(new string[]{"test", "partner_int"});
            AppsFlyerLib.Instance.StartTracking(this, "4UGrDF4vFvPLbHq5bXtCza"); // Replace with your app DevKey
        }

        private void PurchaseButtonClick(object sender, EventArgs eventArgs)
        {
            Dictionary<string, Java.Lang.Object> eventValues = new Dictionary<string, Java.Lang.Object>();
            eventValues.Add(AFInAppEventParameterName.Price, 2);
            eventValues.Add(AFInAppEventParameterName.Currency, "USD");
            eventValues.Add(AFInAppEventParameterName.Quantity, "1");
            AppsFlyerLib.Instance.TrackEvent(this.BaseContext, AFInAppEventType.Purchase, eventValues);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;

            // Correct User Invite implementation
            Com.Appsflyer.Share.LinkGenerator linkGenerator = Com.Appsflyer.Share.ShareInviteHelper.GenerateInviteUrl(view.Context);
            linkGenerator.SetCampaign("my_campaign");
            linkGenerator.AddParameter("af_cost_value", "2.5");
            linkGenerator.AddParameter("af_cost_currency", "USD");
            OneLinkResponseListener listener = new OneLinkResponseListener(view);
            linkGenerator.GenerateLink(view.Context, listener);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


    class OneLinkResponseListener : Java.Lang.Object, CreateOneLinkHttpTask.IResponseListener
    {
        View view;

        public OneLinkResponseListener(View view)
        {
            this.view = view;
        }

        public void OnResponse(string p0)
        {
            string message = "Link generated sucessfully: " + p0;
            Snackbar.Make(view, message, Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            Console.WriteLine(message);
        }

        public void OnResponseError(string p0)
        {
            string message = "Link was NOT generated. Error: " + p0;
            Snackbar.Make(view, message, Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            Console.WriteLine(message);
        }
    }
}

