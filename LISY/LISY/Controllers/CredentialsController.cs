using LISY.DataManagers;
using LISY.Entities.Requests;
using LISY.Entities.Requests.Librarian.Put;
using LISY.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    /// <summary>
    /// Implements all HTTP requests to credentials
    /// </summary>
    [Produces("application/json")]
    [Route("api/Credentials")]
    public class CredentialsController : Controller
    {
        /// <summary>
        /// Tries to authorize user with given login and password and returns his id
        /// </summary>
        /// <param name="login">Given login</param>
        /// <param name="password">Given password</param>
        /// <returns>User id if authorization is successfull and -1 if it is not</returns>
        [Route("authorize")]
        [HttpGet]
        public long Authorize(string login, string password)
        {
            return CredentialsDataManager.Authorize(login, password);
        }        

        /// <summary>
        /// Edits user credentials by given request
        /// </summary>
        /// <param name="request">Given request</param>
        [Route("edit_user_credentials")]
        [HttpPut]
        public void EditUserCredentials([FromBody]EditUserCredentialsRequest request)
        {
            CredentialsDataManager.EditUserCredentials(request.UserId, request.Password);
        }        
    }
}