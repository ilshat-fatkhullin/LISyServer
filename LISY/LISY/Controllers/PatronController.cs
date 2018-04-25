using LISY.DataManagers;
using LISY.Entities.Notifications;
using LISY.Entities.Requests.Patron.Post;
using LISY.Entities.Requests.Patron.Put;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LISY.Controllers
{
    /// <summary>
    /// Implements all HTTP requests to patron
    /// </summary>
    [Produces("application/json")]
    [Route("api/Patron")]
    public class PatronController : Controller
    {
        /// <summary>
        /// Checks out document by given request
        /// </summary>
        /// <param name="request">Given request</param>
        [Route("check_out")]
        [HttpPut]
        public string CheckOutDocument([FromBody]CheckOutDocumentRequest request)
        {
            try
            {
                DocumentsDataManager.CheckOutDocument(request.DocumentId, request.UserId);
                LogsDataManager.SendLog(
                    request.UserId,
                    "Patron",
                    "checked out document with id " + request.DocumentId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }

        /// <summary>
        /// Renews document by given request
        /// </summary>
        /// <param name="request">Given request</param>
        [Route("renew_copy")]
        [HttpPut]
        public void RenewCopy([FromBody]RenewCopyRequest request)
        {
            LogsDataManager.SendLog(
                request.PatronId,
                "Patron",
                "renewed document with id " + request.DocumentId);
            DocumentsDataManager.RenewCopy(request.DocumentId, request.PatronId);
        }

        /// <summary>
        /// Adds patron to queue by given request
        /// </summary>
        /// <param name="request">Given request</param>
        [Route("add_to_queue")]
        [HttpPost]
        public void AddToQueue([FromBody]AddToQueueRequest request)
        {
            LogsDataManager.SendLog(
                request.PatronId,
                "Patron",
                "has been added to queue to document with id " + request.DocumentId);
            UsersDataManager.AddToQueue(request.DocumentId, request.PatronId);
        }

        /// <summary>
        /// Gets notifications of patron with given patron id
        /// </summary>
        /// <param name="patronId">Given patron id</param>
        /// <returns>List of notifications</returns>
        [Route("get_notifications")]
        [HttpGet]
        public Notification[] GetNotifications(long patronId)
        {
            return NotificationsDataManager.GetNotificationsByPatron(patronId);
        }

        /// <summary>
        /// Sets given notification as checked by given patron
        /// </summary>
        /// <param name="request">Given request</param>
        [Route("read_notification")]
        [HttpPut]
        public void ReadNotification([FromBody]ReadNotificationRequest request)
        {
            LogsDataManager.SendLog(
                request.PatronId,
                "Patron",
                "read notification with id " + request.Id);
            NotificationsDataManager.ReadNotification(request.Id);
        }
    }
}