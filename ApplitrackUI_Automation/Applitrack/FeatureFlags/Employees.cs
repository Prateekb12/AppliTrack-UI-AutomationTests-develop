using System;
using System.Configuration;

namespace ApplitrackUITests.FeatureFlags
{
    public static class Employees
    {
        /// <summary>
        /// Determine if HR Files integration is turned on
        /// </summary>
        public static bool UsesEmployees => Convert.ToBoolean(ConfigurationManager.AppSettings["Employees.UsesEmployees"]);
    }
}
