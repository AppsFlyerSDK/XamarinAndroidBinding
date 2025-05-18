using System.Reflection;
using Com.Appsflyer.Internal.Platform_extension;
using Android.Util;

namespace Com.Appsflyer
{
    partial class AppsFlyerLib
    {
        static bool didSetPlugin = false;
        const string LOG_TAG = "AppsFlyer_Xamarin";

        public static AppsFlyerLib Instance
        {
            get
            {
                var Instance = AppsFlyerLib.__Instance();
                if (didSetPlugin)
                {
                    return Instance;
                }
                var assembly = typeof(AppsFlyerLib).GetTypeInfo().Assembly;
                var assemblyName = new AssemblyName(assembly.FullName);
                var version = assemblyName.Version;
                var versionStr = version.ToString();
                var extra = new Dictionary<string, string>();
                extra["build"] = version.Build.ToString();
                extra["revision"] = version.Revision.ToString();
                
                Plugin? pluginValue = null;
                try
                {
                    pluginValue = Plugin.ValueOf("XAMARIN");
                    
                    if (pluginValue != null)
                    {
                        Instance.SetPluginInfo(new PluginInfo(pluginValue, versionStr, extra));
                        didSetPlugin = true;
                    }
                    else
                    {
                         Log.Error(LOG_TAG, "Plugin.ValueOf('XAMARIN') returned null.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(LOG_TAG, $"Error getting/setting Plugin info using ValueOf: {ex.Message}");
                }
                
                return Instance;
            }
        }
    }
}

