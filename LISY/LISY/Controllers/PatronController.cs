using LISY.DataManagers;
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
    }
}