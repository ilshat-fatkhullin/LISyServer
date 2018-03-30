using LISY.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Patron")]
    public class PatronController : Controller
    {
        [Route("check_out")]
        [HttpPut]
        public void CheckOutDocument(long documentId, long userId)
        {
            DocumentsDataManager.CheckOutDocument(documentId, userId);
        }
    }
}