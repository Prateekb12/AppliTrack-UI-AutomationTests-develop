using System;
using System.Configuration;

namespace ApplitrackUITests.Helpers
{
    public static class TestEnvironment
    {
        private static readonly string ConfigEnvironment = Convert.ToString(ConfigurationManager.AppSettings["EnvironmentType"]).ToLower();

        public static EnvironmentType CurrentEnvironment
        {
            get
            {
                switch (ConfigEnvironment)
                {
                    case "localhost":
                        return EnvironmentType.LocalHost;
                    case "awsqa":
                        return EnvironmentType.AwsQA;
                    case "qa":
                        return EnvironmentType.QA;
                    case "stage":
                        return EnvironmentType.Staging;
                    case "prod":
                        return EnvironmentType.Production;
                    default:
                        return EnvironmentType.Production;
                }
            }
        }

        public static string ClientCode { get; } = Convert.ToString(ConfigurationManager.AppSettings["ClientCode"]).ToLower();

        public static string DefaultUserType { get; } = Convert.ToString(ConfigurationManager.AppSettings["IDM.DefaultUserType"]);
    }
}