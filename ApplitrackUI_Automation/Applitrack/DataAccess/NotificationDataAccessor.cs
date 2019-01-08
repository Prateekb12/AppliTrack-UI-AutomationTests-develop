using System.Collections.Generic;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class NotificationDataAccessor : BaseDataAccessor
    {
        /// <summary>
        /// Retrieve all notifications for a given user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<Notification> GetNotificationsForUser(string userName)
        {
            //Apparently this is the syntax one must use to make OrmLite happy...
            return _readWriteData.Find<Notification>(n => n.UserId.ToLower() == userName.ToLower());
        }

        /// <summary>
        /// Deletes the notification status record matching the objectId
        /// </summary>
        /// <param name="objectId"></param>
        public void DeleteNotificationStatusByObjectId(int objectId)
        {
            _readWriteData.Delete<NotificationStatus>(ns => ns.ObjectId == objectId);
        }
    }
}
