using System.Collections.Generic;
using System.Linq;
using ApplitrackUITests.DataModels;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.DataGenerators
{
    public static class IdmLoginData
    {
        private static readonly IEnumerable<IUser> AwsQaUsers = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "awsqa-autosuper",
                Password = "awsqa-autosuper1"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "awsqa-autostandard1",
                Password = "awsqa-autostandard1"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "awsqa-autorouting1",
                Password = "awsqa-autorouting1"
            }
        };

        /// <summary>
        /// List of IDM users for the QA environment
        /// </summary>
        private static readonly IEnumerable<IUser> QaUsers = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "qa-autosuper",
                Password = "qa-autosuper1"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "qa-autostandard",
                Password = "qa-autostandard1"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "qa-autorouting",
                Password = "qa-autorouting1"
            }
        };

        /// <summary>
        /// List of IDM users for the Production environment
        /// </summary>
        private static readonly IEnumerable<IUser> ProductionUsers = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "prod-autosuper",
                Password = "prod-autosuper1"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "prod-autostandard",
                Password = "prod-autostandard1"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "prod-autorouting",
                Password = "prod-autorouting1"
            }
        };

        /// <summary>
        /// List of IDM users for the Staging environment
        /// </summary>
        private static readonly IEnumerable<IUser> StagingUsers = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "stage-autosuper",
                Password = "stage-autosuper1"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "stage-autostandard",
                Password = "stage-autostandard1"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "stage-autorouting",
                Password = "stage-autorouting1"
            }
        };

        /// <summary>
        /// Dictionary of users for each environment: QA, Production, and Staging
        /// </summary>
        private static readonly IDictionary<EnvironmentType, IEnumerable<IUser>> Users =
            new Dictionary<EnvironmentType, IEnumerable<IUser>>
            {
                { EnvironmentType.LocalHost, QaUsers },
                { EnvironmentType.Production, ProductionUsers },
                { EnvironmentType.QA, QaUsers },
                { EnvironmentType.AwsQA, AwsQaUsers },
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
