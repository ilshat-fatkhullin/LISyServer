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
        public static long Authorize(string login, string password)
        {
            return CredentialsDataManager.Authorize(login, password);
        }

        [Route("get_user_type")]
        [HttpGet]
        public static string GetUserType(long userId)
        {
            return CredentialsDataManager.GetUserType(userId);
        }

        [Route("add_user_credentials")]
        [HttpGet]
        public static long AddUserCredentials(string login, string password)
        {
            return CredentialsDataManager.AddUserCredentials(login, password);
        }

        [Route("delete_user_credentials")]
        [HttpDelete]
        public static void DeleteUserCredentials(long userId)
        {
            CredentialsDataManager.DeleteUserCredentials(userId);
        }

        [Route("edit_user_credentials")]
        [HttpPut]
        public static void ModifyUserCredentials(long userId, string password)
        {
            CredentialsDataManager.ModifyUserCredentials(userId, password);
        }

        [Route("get_user_by_id")]
        [HttpGet]
        public static User GetUserByID(long userId)
        {
            return CredentialsDataManager.GetUserByID(userId);
        }
    }
}