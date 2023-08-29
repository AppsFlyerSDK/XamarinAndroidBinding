using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Appsflyer.Internal.Platform_extension;

namespace Com.Appsflyer
{
    partial class AppsFlyerLib
    {
        static bool didSetPlugin = false;

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
                Instance.SetPluginInfo(new PluginInfo(Plugin.Xamarin, versionStr, extra));
                didSetPlugin = true;
                return Instance;
            }
        }
    }
}

