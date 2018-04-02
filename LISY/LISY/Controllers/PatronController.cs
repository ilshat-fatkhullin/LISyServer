using LISY.DataManagers;
using LISY.Entities.Notifications;
using LISY.Entities.Requests.Patron.Post;
using LISY.Entities.Requests.Patron.Put;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Patron")]
    public class PatronController : Controller
    {
        [Route("check_out")]
        [HttpPut]
        public void CheckOutDocument([FromBody]CheckOutDocumentRequest request)
        {
            DocumentsDataManager.CheckOutDocument(request.DocumentId, request.UserId);
        }

        [Route("renew_copy")]
        [HttpPut]
        public void RenewCopy([FromBody]RenewCopyRequest request)
        {
            DocumentsDataManager.RenewCopy(request.DocumentId, request.PatronId);
        }

        [Route("add_to_queue")]
        [HttpPost]
        public void AddToQueue([FromBody]AddToQueueRequest request)
        {
            UsersDataManager.AddToQueue(request.DocumentId, request.PatronId);
        }

        [Route("get_notifications")]
        [HttpGet]
        public Notification[] GetNotifications(long patronId)
        {
            return NotificationsDataManager.GetNotificationsByPatron(patronId);
        }

        [Route("read_notification")]
        [HttpPut]
        public void ReadNotification([FromBody]ReadNotificationRequest request)
        {
            NotificationsDataManager.ReadNotification(request.Id);
        }
    }
}