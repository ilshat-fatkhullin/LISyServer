using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {        
        [HttpGet]
        public string Get()
        {
            return "LISy API.";
        }
    }
}