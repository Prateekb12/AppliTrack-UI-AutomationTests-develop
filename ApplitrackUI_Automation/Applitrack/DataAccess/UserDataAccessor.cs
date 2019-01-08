using System;
using Recruit.Cache.Connections.Implementations;
using Recruit.Persistence;
using Recruit.Persistence.Implementations;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class UserDataAccessor : BaseDataAccessor
    {
        /// <summary>
        /// Create user using database calls using default permissions
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <returns>The user ID</returns>
        public long CreateUser(RecruitUser user)
        {
            var defaultPermissions = new Permission()
            {
                System = "OnlineApplication",
                Scope = "System",
                PermissionName = "SuperUser",
                UserId = user.UserName,
                ObjectId = String.Empty
            };
            return CreateUser(user, defaultPermissions);
        }

        /// <summary>
        /// Create a user using database calls with the given permissions
        /// </summary>
        /// <param name="userData">The user to create</param>
        /// <param name="permissions">The users permissions</param>
        /// <returns>The user ID</returns>
        public long CreateUser(RecruitUser user, Permission permissions)
        {
            var id = _readWriteData.Create(user, true);
            _readWriteData.Create(permissions);
            return id;
        }

        /// <summary>
        /// Get a user with the given ID
        /// </summary>
        /// <param name="id">The ID of the user to return</param>
        /// <returns>A user with the given ID</returns>
        public RecruitUser GetUser(long id)
        {
            return _readWriteData.FindOne<RecruitUser>(u => u.Id == id);
        }

        /// <summary>
        /// Get a user with the given user name
        /// </summary>
        /// <param name="userName">The user name of the user to return</param>
        /// <returns>A user with the given user name</returns>
        public RecruitUser GetUser(string userName)
        {
            return _readWriteData.FindOne<RecruitUser>(u => u.UserName == userName);
        }

        /// <summary>
        /// Delete a given user
        /// </summary>
        /// <param name="user">The user object of the user to delete</param>
        public void DeleteUser(RecruitUser user)
        {
            _readWriteData.Delete<RecruitUser>(user);
        }

        /// <summary>
        /// Update a given user
        /// </summary>
        /// <param name="user">An object containing the user updates</param>
        public void UpdateUser(RecruitUser user)
        {
            _readWriteData.Update<RecruitUser>(user);
        }
    }
}
