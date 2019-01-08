using System;
using System.Configuration;

namespace ApplitrackUITests.FeatureFlags
{
    public static class PersonalInfo
    {
        /// <summary>
        /// Determine if the Social Security Number fields appear for applicants on the 'Personal Info' page
        /// </summary>
        public static bool CollectSSN => Convert.ToBoolean(ConfigurationManager.AppSettings["PersonalInfo.CollectSSN"]);
    }
}
