using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Appsflyer;
using System.Collections.Generic;

namespace AFXamarinAndroidSampleApp
{
	[Activity (Label = "AFXamarinAndroidSampleApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			Console.WriteLine ("AppsFlyerXamarinAndroidSampleApp onCreate");
			base.OnCreate (bundle);

			AppsFlyerLib.SetAppsFlyerKey ("rbz2mfgZQY5mSEYNTyjwni"); 
			AppsFlyerLib.SendTracking (this);

			//AppsFlyerLib.GetConversionData (this);
			AppsFlyerLib.RegisterConversionListener (this, new AppsFlyerConversionDelegate ());

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate {
				button.Text = string.Format ("{0} Add to cart sent", count++);

				AppsFlyerLib.TrackEvent(this, AFInAppEventType.AddToCart, new Dictionary<string, Java.Lang.Object> { 
					{AFInAppEventParameterName.ContentId, "id1234"}, 
					{AFInAppEventParameterName.ContentType, "tickets"},
					{AFInAppEventParameterName.Price, 123},
					{AFInAppEventParameterName.Currency, "USD"},
				});

			};
		}
	}
}


