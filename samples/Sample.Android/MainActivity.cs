using Android.App;
using Android.OS;
using Com.Appsflyer;
using Java.Lang;
using Android.Widget;
using System;
using Android.Views;
using System.Collections.Generic;

namespace Sample.Android
{
    [Activity(Label = "AppsFlyer SDK Demo", MainLauncher = true, Theme = "@android:style/Theme.Material.Light", Name = "com.appsflyer.xamarinsample.MainActivity")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set content view
            SetContentView(CreateDemoLayout());
            
            // Initialize AppsFlyer SDK 6.17.0
            string devKey = "4UGrDF4vFvPLbHq5bXtCza"; // Replace with your AppsFlyer Dev Key
            AppsFlyerLib appsFlyerLib = AppsFlyerLib.Instance;
            appsFlyerLib.Init(devKey, null, this);
            appsFlyerLib.Start(this);
            
            // Enable debug logs in development
            appsFlyerLib.SetDebugLog(true);
            
            System.Console.WriteLine("AppsFlyer SDK Initialized");
            
            // Get button and attach click handler
            Button button = FindViewById<Button>(1001);
            int clickCount = 0;
            
            button.Click += (sender, e) => {
                clickCount++;
                button.Text = $"Clicked {clickCount} times";
                
                // Log event to AppsFlyer - convert HashMap to Dictionary for the API
                var eventValues = new Dictionary<string, Java.Lang.Object>();
                eventValues["button_click_count"] = new Java.Lang.String(clickCount.ToString());
                eventValues["timestamp"] = new Java.Lang.String(DateTime.Now.ToString("o"));
                
                // Log the event to AppsFlyer
                AppsFlyerLib.Instance.LogEvent(this, "button_click", eventValues);
                
                System.Console.WriteLine($"Logged AppsFlyer event: button_click (count: {clickCount})");
            };
        }
        
        protected override void OnResume()
        {
            base.OnResume();
            
            // Start the SDK (will be called on each activity resume)
            AppsFlyerLib.Instance.Start(this);
        }
        
        private View CreateDemoLayout()
        {
            // Create a simple linear layout
            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            layout.SetPadding(40, 40, 40, 40);
            layout.SetGravity(GravityFlags.Center);
            
            // Create title text
            var titleText = new TextView(this);
            titleText.Text = "AppsFlyer SDK 6.17.0 Demo";
            titleText.TextSize = 24;
            titleText.SetPadding(0, 0, 0, 80);
            titleText.Gravity = GravityFlags.Center;
            
            // Create button
            var button = new Button(this);
            button.Id = 1001;
            button.Text = "Click me";
            
            // Add views to layout
            layout.AddView(titleText);
            layout.AddView(button);
            
            return layout;
        }
    }
} 