using System;
using System.Configuration;

namespace ApplitrackUITests.FeatureFlags
{
    public static class ThirdParty
    {
        internal static class Aesop
        {
            /// <summary>
            /// Determine if Aesop integration is enabled
            /// </summary>
            public static bool Enabled => Convert.ToBoolean(ConfigurationManager.AppSettings["ThirdParty.Aesop.Enabled"]);
        }
    }
}
