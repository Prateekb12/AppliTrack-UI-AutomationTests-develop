using System.Collections.Generic;
using System.Linq;
using ApplitrackUITests.DataModels;

namespace ApplitrackUITests.DataGenerators
{
    public static class StandardLoginData
    {
        /// <summary>
        /// A list of users for the Standard (non-IDM) environment
        /// </summary>
        private static readonly IEnumerable<IUser> Users = new List<IUser>
        {
            new UserGenerator
            {
                Type = UserType.SuperUser,
                UserName = "tryout",
                Password = "tryout"
            },
            new UserGenerator
            {
                Type = UserType.Standard,
                UserName = "tryout",
                Password = "tryout"
            },
            new UserGenerator
            {
                Type = UserType.RoutingsOnly,
                UserName = "tryout",
                Password = "tryout"
            }
        };
        /// <summary>
        /// Retreive the specified user record
        /// </summary>
        /// <param name="userType">The type of user to return</param>
        /// <returns>A user record for the specified user type</returns>
        public static IUser GetUser(UserType userType)
        {
            return Users.First(user => user.Type == userType);
        }
    }
}
