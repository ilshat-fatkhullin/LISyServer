using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LISY.DataManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Patron")]
    public class PatronController : Controller
    {
        [Route("check_out")]
        [HttpPut]
        public static void CheckOutDocument(long documentId, long userId)
        {
            DocumentsDataManager.CheckOutDocument(documentId, userId);
        }
    }
}