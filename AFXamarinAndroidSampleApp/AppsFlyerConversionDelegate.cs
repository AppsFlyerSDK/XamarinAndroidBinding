using System;
using Com.Appsflyer;
using System.Collections.Generic;

namespace AFXamarinAndroidSampleApp
{
	public class AppsFlyerConversionDelegate : Java.Lang.Object, IAppsFlyerConversionListener
	{	
		public AppsFlyerConversionDelegate ()
		{
			Console.WriteLine ("AppsFlyerConversionDelegate called");

		}

		public void OnAppOpenAttribution (IDictionary<string, string> p0) {
			Console.WriteLine ("OnAppOpenAttribution = " + p0.ToString());

		}

		public void OnAttributionFailure (string p0) {
			Console.WriteLine ("OnAttributionFailure = " + p0);

		}

		public void OnInstallConversionDataLoaded (IDictionary<string, string> p0) {
			foreach (var kvp in p0) {
				Console.WriteLine (kvp.Key + " = " + kvp.Value);
			}
		}

		public void OnInstallConversionFailure (string p0) {
			Console.WriteLine ("OnInstallConversionFailure = " + p0);

		}
	}
}

