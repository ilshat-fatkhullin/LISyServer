using LISY.Helpers;
using System.Linq;

namespace LISY.DataManagers
{
    /// <summary>
    /// Cr
    /// </summary>
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
    }
}
