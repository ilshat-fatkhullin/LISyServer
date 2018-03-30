using LISY.Entities.Documents;
using LISY.Entities.Users;
using LISY.Helpers;
using System;
using System.Linq;

namespace LISY.DataManagers
{
    public static class UsersDataManager
    {
        public static bool AddUser(User user, string login, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var output = DatabaseHelper.Query<bool>("dbo.spUsers_IsUserInTable @FirstName, @SecondName, @Phone",
                new { FirstName = user.FirstName, SecondName = user.SecondName, Phone = user.Phone }).ToList();
            if (!output[0])
            {
                long cardNumber = CredentialsDataManager.AddUserCredentials(login, password);

                user.CardNumber = cardNumber;
                DatabaseHelper.Execute("dbo.spUsers_AddUser @FirstName, @SecondName, @CardNumber, @Phone, @Address, @Type",
                        new
                        {
                            FirstName = user.FirstName,
                            SecondName = user.SecondName,
                            CardNumber = user.CardNumber,
                            Phone = user.Phone,
                            Address = user.Address,
                            Type = user.GetType().ToString().Split('.').Last()
                        });
                return true;
            }
            else
            {
                return false;
            }
        }        

        public static void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            CredentialsDataManager.DeleteUserCredentials(user.CardNumber);

            DatabaseHelper.Execute("dbo.spUsers_DeleteUser @CardNumber", user);
        }

        public static void EditUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException();
            }

            DatabaseHelper.Execute("dbo.spUsers_ModifyUser @CardNumber, @FirstName, @SecondName, @Phone, @Address",
                    new
                    {
                        CardNumber = newUser.CardNumber,
                        FirstName = newUser.FirstName,
                        SecondName = newUser.SecondName,
                        Phone = newUser.Phone,
                        Address = newUser.Address
                    });
        }

        public static int GetNumberOfUsers()
        {
            var output = DatabaseHelper.Query<int>("dbo.spUsers_GetNumberOfUsers", null).ToList();
            return (output[0]);
        }

        public static User[] GetUsersList()
        {
            return DatabaseHelper.Query<User>("dbo.spUsers_GetAllUsers", null).ToArray();
        }
    }
}
