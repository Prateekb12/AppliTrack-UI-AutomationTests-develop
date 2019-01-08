using System.Collections.Generic;
using System.Linq;
using ApplitrackUITests.DataModels;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.DataGenerators
{
    public static class JeffersonLoginData
    {
        /// <summary>
        /// List of IDM users for the Staging environment
        /// </summary>
        private static readonly IEnumerable<IUser> StagingUsers = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "recruituser1",
                Password = "q1234567"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "recruituser4std",
                Password = "q1234567"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "recruituser5route",
                Password = "q1234567"
            }
        };

        /// <summary>
        /// Dictionary of users for each environment: QA, Production, and Staging
        /// </summary>
        private static readonly IDictionary<EnvironmentType, IEnumerable<IUser>> Users =
            new Dictionary<EnvironmentType, IEnumerable<IUser>>
            {
                { EnvironmentType.Staging, StagingUsers }
            };

        /// <summary>
        /// Retreive the specified user record
        /// </summary>
        /// <param name="userType">The type of user to return</param>
        /// <returns>A user record for the specified user type</returns>
        public static IUser GetUser(UserType userType)
        {
            return Users[TestEnvironment.CurrentEnvironment]
                .First(user => user.Type == userType);
        }
    }
}
