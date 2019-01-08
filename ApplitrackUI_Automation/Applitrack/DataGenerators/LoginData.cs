using System;
using System.Configuration;
using ApplitrackUITests.DataModels;

namespace ApplitrackUITests.DataGenerators
{
    public static class LoginData
    {
        private static readonly bool IdmEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IDMEnabled"]);
        private static readonly bool IsJefferson = Convert.ToBoolean(ConfigurationManager.AppSettings["IsJefferson"]);

        private static IUser GetUser(UserType userType)
        {
            if (IdmEnabled)
            {
                return IdmLoginData.GetUser(userType);
            }
            if (IsJefferson)
            {
                return JeffersonLoginData.GetUser(userType);
            }
            return StandardLoginData.GetUser(userType);
        }

        /// <summary>
        /// User name for the super user used for testing
        /// </summary>
        public static string SuperUserName => GetUser(UserType.SuperUser).UserName;

        /// <summary>
        /// Password for a super user used for testing
        /// </summary>
        public static string SuperUserPassword => GetUser(UserType.SuperUser).Password;

        /// <summary>
        /// User name for the standard user used for testing
        /// </summary>
        public static string StdUserName => GetUser(UserType.Standard).UserName;

        /// <summary>
        /// Password for the standard user used for testing
        /// </summary>
        public static string StdUserPassword => GetUser(UserType.Standard).Password;

        /// <summary>
        /// Password for the routing user used for testing
        /// </summary>
        public static string RoutingUserName => GetUser(UserType.RoutingsOnly).UserName;

        /// <summary>
        /// User name for the routing user used for testing
        /// </summary>
        public static string RoutingUserPassword => GetUser(UserType.RoutingsOnly).Password;
    }
}
