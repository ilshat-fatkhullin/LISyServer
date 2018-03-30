using LISY.DataManagers;
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

        [Route("get_user_type")]
        [HttpGet]
        public string GetUserType(long userId)
        {
            return CredentialsDataManager.GetUserType(userId);
        }

        [Route("add_user_credentials")]
        [HttpPost]
        public long AddUserCredentials(string login, string password)
        {
            return CredentialsDataManager.AddUserCredentials(login, password);
        }

        [Route("delete_user_credentials")]
        [HttpDelete]
        public void DeleteUserCredentials(long userId)
        {
            CredentialsDataManager.DeleteUserCredentials(userId);
        }

        [Route("edit_user_credentials")]
        [HttpPut]
        public void EditUserCredentials(long userId, string password)
        {
            CredentialsDataManager.EditUserCredentials(userId, password);
        }

        [Route("get_user_by_id")]
        [HttpGet]
        public User GetUserByID(long userId)
        {
            return CredentialsDataManager.GetUserByID(userId);
        }
    }
}