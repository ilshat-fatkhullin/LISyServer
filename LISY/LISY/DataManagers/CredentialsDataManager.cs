using LISY.Entities.Users;
using LISY.Helpers;
using System;
using System.Linq;

namespace LISY.DataManagers
{
    public class CredentialsDataManager
    {
        public static long Authorize(string login, string password)
        {
            var output = DatabaseHelper.Query<long>("dbo.spCredentials_Authorize @Login, @Password", new
            {
                Login = login,
                Password = password
            }).ToList();
            if (output.Count > 0)
            {
                return output[0];
            }

            return -1;
        }

        public static string GetUserType(long userId)
        {
            var output = DatabaseHelper.Query<String>("dbo.spUsers_GetUserTypeByCard  @CardNumber", new { CardNumber = userId }).ToList();
            return output[0];
        }

        public static long AddUserCredentials(string login, string password)
        {
            var output = DatabaseHelper.Query<long>("dbo.spCredentials_AddCredential @Login, @Password", new { Login = login, Password = password }).ToList();
            return output[0];
        }

        public static void DeleteUserCredentials(long userId)
        {
            DatabaseHelper.Execute("dbo.spCredentials_DeleteCredential @CardNumber", new { CardNumber = userId });
        }

        public static void EditUserCredentials(long userId, string password)
        {
            DatabaseHelper.Execute("dbo.spCredentials_ModifyCredential @CardNumber, @Password",
                    new { CardNumber = userId, Password = password });
        }

        public static User GetUserByID(long userID)
        {
            return DatabaseHelper.Query<User>("dbo.spUsers_GetUserById @Id", new { Id = userID }).FirstOrDefault();
        }
    }
}
