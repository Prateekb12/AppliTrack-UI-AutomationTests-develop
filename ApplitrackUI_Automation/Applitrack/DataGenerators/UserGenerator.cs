using System;
using ApplitrackUITests.DataAccess;
using ApplitrackUITests.DataModels;
using ApplitrackUITests.Helpers;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataGenerators
{
    /// <summary>
    /// Assign or randomly generate data for a user
    /// </summary>
    public class UserGenerator : PersonGenerator, IUser
    {
        private string _userName;

        public string UserName
        {
            // prefix this with "AAA" to put it at the top of the list - makes it quicker/easier to find
            get { return _userName ?? (_userName = "AAA" + Faker.Internet.UserName()); }
            set { _userName = value; }
        }

        private UserType _userType;

        public UserType Type
        {
            get { return _userType; }
            set { _userType = value; }
        }

        public long Id { get; set; }

        private readonly UserDataAccessor _userDataAccessor = new UserDataAccessor();

        /// <summary>
        /// Create user record in the database
        /// </summary>
        public void CreateInDatabase()
        {
            var user = new RecruitUser()
            {
                UserName = this.UserName,
                Email = this.Email,
                FullName = this.RealName
            };

            var permissions = new Permission()
            {
                System = "OnlineApplication",
                Scope = "System",
                PermissionName = this.Type.ToString(),
                UserId = user.UserName,
                ObjectId = String.Empty
            };

            this.Id = _userDataAccessor.CreateUser(user, permissions);
        }

        /// <summary>
        /// Delete user record from the database
        /// </summary>
        public void DeleteFromDatabase()
        {
            _userDataAccessor.DeleteUser(_userDataAccessor.GetUser(this.Id));
        }
    }
}
