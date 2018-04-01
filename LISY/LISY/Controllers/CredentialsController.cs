using LISY.DataManagers;
using LISY.Entities.Requests;
using LISY.Entities.Requests.Librarian.Put;
using LISY.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Credentials")]
    public class CredentialsController : Controller
    {
        [Route("authorize")]
        [HttpGet]
        public long Authorize(string login, string password)
        {
            return CredentialsDataManager.Authorize(login, password);
        }        

        [Route("edit_user_credentials")]
        [HttpPut]
        public void EditUserCredentials([FromBody]EditUserCredentialsRequest request)
        {
            CredentialsDataManager.EditUserCredentials(request.UserId, request.Password);
        }        
    }
}