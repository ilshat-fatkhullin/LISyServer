using LISY.Entities.Users;
using LISY.Entities.Users.Patrons;
using LISY.Helpers;
using System;
using System.Collections;
using System.Linq;

namespace LISY.DataManagers
{
    public static class UsersDataManager
    {
        private static bool AddUser(User user, string login, string password)
        {
            bool output = DatabaseHelper.Query<bool>("dbo.spUsers_IsUserInTable @FirstName, @SecondName, @Phone",
                new
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Phone = user.Phone
                }).ToList().FirstOrDefault();
            if (!output)
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

        public static bool AddLibrarian(Librarian librarian, string login, string password)
        {
            return AddUser(librarian, login, password);
        }

        public static bool AddFaculty(Faculty faculty, string login, string password)
        {
            return AddUser(faculty, login, password);
        }

        public static bool AddStudent(Student student, string login, string password)
        {
            return AddUser(student, login, password);
        }

        public static bool AddGuest(Guest guest, string login, string password)
        {
            return AddUser(guest, login, password);
        }

        private static void EditUser(User newUser)
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

        public static void EditLibrarian(Librarian librarian)
        {
            EditUser(librarian);
        }

        public static void EditFaculty(Faculty faculty)
        {
            EditUser(faculty);
        }

        public static void EditStudent(Student student)
        {
            EditStudent(student);
        }

        public static void EditGuest(Guest guest)
        {
            EditUser(guest);
        }

        public static void DeleteUser(long userId)
        {            
            CredentialsDataManager.DeleteUserCredentials(userId);
            DatabaseHelper.Execute("dbo.spUsers_DeleteUser @CardNumber", CredentialsDataManager.GetUserByID(userId));
        }        

        public static int GetNumberOfUsers()
        {
            var output = DatabaseHelper.Query<int>("dbo.spUsers_GetNumberOfUsers", null).ToList();
            return (output[0]);
        }

        public static User[] GetUsersList()
        {
            var output = DatabaseHelper.Query<User>("dbo.spUsers_GetAllUsers", null);
            if (output == null)
                return new User[] { };
            return output.ToArray();
        }

        public static Patron[] GetQueueToDocument(long documentId)
        {
            var output = DatabaseHelper.Query<Patron>("dbo.spQueue_GetQueueByDocumentId @DocumentId", new { DocumentId = documentId });
            if (output == null)
                return new Patron[] { };
            return output.ToArray();
        }
    }
}
