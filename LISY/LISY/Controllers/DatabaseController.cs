using LISY.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Database")]
    public class DatabaseController : Controller
    {
        [HttpDelete]
        [Route("delete")]
        public void Delete()
        {
            DatabaseDataManager.ClearAll();
        }        
    }
}