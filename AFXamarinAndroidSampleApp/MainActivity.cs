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
			SetContentView (Resource.Layout.Main);

			Console.WriteLine ("AppsFlyerXamarinAndroidSampleApp onCreate");
			base.OnCreate (bundle);

			AppsFlyerLib.Instance.StartTracking (this.Application, "rbz2mfgZQY5mSEYNTyjwni");
			AppsFlyerLib.Instance.RegisterConversionListener (this, new AppsFlyerConversionDelegate ());

			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				button.Text = string.Format ("{0} Add to cart sent", count++);

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


