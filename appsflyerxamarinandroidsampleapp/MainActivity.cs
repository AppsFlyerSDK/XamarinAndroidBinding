using Android.App;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;

using Com.Appsflyer;

namespace appsflyerxamarinandroidsampleapp
{
    [Activity(Label = "appsflyerxamarinandroidsampleapp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AppsFlyerLib.Instance.SetDebugLog(true);
            AppsFlyerLib.Instance.StartTracking(this.Application, "rbz2mfgZQY5mSEYNTyjwni");
            AppsFlyerLib.Instance.RegisterConversionListener(this, new AppsFlyerConversionDelegate());

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { 
                button.Text = string.Format("{0} Send AddToCart Event", count++);

                AppsFlyerLib.Instance.TrackEvent(this, AFInAppEventType.AddToCart, new Dictionary<string, Java.Lang.Object> {
                    {AFInAppEventParameterName.ContentId, "id1234"},
                    {AFInAppEventParameterName.ContentType, "tickets"},
                    {AFInAppEventParameterName.Price, 123},
                    {AFInAppEventParameterName.Currency, "USD"},
                });
            };
        }
    }
}

