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
    [Route("api/Database")]
    public class DatabaseController : Controller
    {
        [HttpGet]
        [Route("delete")]
        public void Delete()
        {
            DatabaseDataManager.ClearAll();
        }        
    }
}