using LISY.Entities.Notifications;
using LISY.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace LISY.DataManagers
{
    public class NotificationsDataManager
    {
        public static void AddNotification(Notification notification)
        {
            DatabaseHelper.Execute("dbo.spNotifications_AddNotification @PatronId, @Message", new { PatronId = notification.PatronId, Message = notification.Message });
        }

        public static Notification[] GetNotificationsByPatron(long patronId)
        {
            var output = DatabaseHelper.Query<Notification>("dbo.spNotifications_GetNotificationsByPatron @PatronId", new { PatronId = patronId });
            if (output == null)
                return new Notification[] { };
            return output.ToArray();
        }

        public static void ReadNotification(long notificationId)
        {
            DatabaseHelper.Execute("dbo.spNotifications_ReadNotification @Id", new { Id = notificationId });
        }
    }
}
