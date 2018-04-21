using LISY.DataManagers;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    /// <summary>
    /// Implements all HTTP requests to database
    /// </summary>
    [Produces("application/json")]
    [Route("api/Database")]
    public class DatabaseController : Controller
    {
        /// <summary>
        /// Clears all tables in database (do not change its structure)
        /// </summary>
        [HttpDelete]
        [Route("delete")]
        public void Delete()
        {
            DatabaseDataManager.ClearAll();
        }
    }
}