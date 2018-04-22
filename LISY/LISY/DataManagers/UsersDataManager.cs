using LISY.Entities.Documents;
using LISY.Entities.Fines;
using LISY.Entities.Users;
using LISY.Entities.Users.Patrons;
using LISY.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LISY.DataManagers
{
    /// <summary>
    /// Contains all functions for users to work with database
    /// </summary>
    public static class UsersDataManager
    {
        private static bool IsUserInTable(User user)
        {
            return DatabaseHelper.Query<bool>("dbo.spUsers_IsUserInTable @FirstName, @SecondName, @Phone",
                new
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Phone = user.Phone
                }).ToList().FirstOrDefault();
        }        

        public static bool AddLibrarian(Librarian librarian, string login, string password)
        {
            bool isUserInTable = IsUserInTable(librarian);
            if (!isUserInTable)
            {
                long cardNumber = CredentialsDataManager.AddUserCredentials(login, password);
                librarian.CardNumber = cardNumber;
                DatabaseHelper.Execute("dbo.spLibrarians_AddLibrarian @FirstName, @SecondName, @CardNumber, @Phone, @Address, @Authority",
                        new
                        {
                            FirstName = librarian.FirstName,
                            SecondName = librarian.SecondName,
                            CardNumber = librarian.CardNumber,
                            Phone = librarian.Phone,
                            Address = librarian.Address,
                            Authority = librarian.Authority
                        });
            }
            return !isUserInTable;
        }

        public static bool AddFaculty(Faculty faculty, string login, string password)
        {
            bool isUserInTable = IsUserInTable(faculty);
            if (!isUserInTable)
            {
                long cardNumber = CredentialsDataManager.AddUserCredentials(login, password);
                faculty.CardNumber = cardNumber;
                DatabaseHelper.Execute("dbo.spPatrons_AddPatron @FirstName, @SecondName, @CardNumber, @Phone, @Address, @Priority, @Type",
                        new
                        {
                            FirstName = faculty.FirstName,
                            SecondName = faculty.SecondName,
                            CardNumber = faculty.CardNumber,
                            Phone = faculty.Phone,
                            Address = faculty.Address,
                            Priority = faculty.Priority,
                            Type = "Faculty"
                        });
            }
            return !isUserInTable;
        }

        public static bool AddStudent(Student student, string login, string password)
        {
            bool isUserInTable = IsUserInTable(student);
            if (!isUserInTable)
            {
                long cardNumber = CredentialsDataManager.AddUserCredentials(login, password);
                student.CardNumber = cardNumber;
                DatabaseHelper.Execute("dbo.spPatrons_AddPatron @FirstName, @SecondName, @CardNumber, @Phone, @Address, @Priority, @Type",
                        new
                        {
                            FirstName = student.FirstName,
                            SecondName = student.SecondName,
                            CardNumber = student.CardNumber,
                            Phone = student.Phone,
                            Address = student.Address,
                            Priority = student.Priority,
                            Type = "Student"
                        });
            }
            return !isUserInTable;
        }

        public static bool AddGuest(Guest guest, string login, string password)
        {
            bool isUserInTable = IsUserInTable(guest);
            if (!isUserInTable)
            {
                long cardNumber = CredentialsDataManager.AddUserCredentials(login, password);
                guest.CardNumber = cardNumber;
                DatabaseHelper.Execute("dbo.spPatrons_AddPatron @FirstName, @SecondName, @CardNumber, @Phone, @Address, @Priority, @Type",
                        new
                        {
                            FirstName = guest.FirstName,
                            SecondName = guest.SecondName,
                            CardNumber = guest.CardNumber,
                            Phone = guest.Phone,
                            Address = guest.Address,
                            Priority = guest.Priority,
                            Type = "Guest"
                        });
            }
            return !isUserInTable;
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
            DatabaseHelper.Execute("dbo.spUsers_DeleteUser @CardNumber", GetUserById(userId));
        }

        public static User GetUserById(long userId)
        {
            return DatabaseHelper.Query<User>("dbo.spUsers_GetUserById @Id", new { Id = userId }).FirstOrDefault();
        }

        public static Librarian GetLibrarianById(long userId)
        {
            return DatabaseHelper.Query<Librarian>("dbo.spLibrarians_GetLibrarianById @Id", new { Id = userId }).FirstOrDefault();
        }

        public static Patron GetPatronById(long patronId)
        {            
            return DatabaseHelper.Query<Patron>("dbo.spPatrons_GetPatronById @Id", new { Id = patronId }).FirstOrDefault();
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

        public static Librarian[] GetLibrariansList()
        {
            var output = DatabaseHelper.Query<Librarian>("dbo.spLibrarians_GetAllLibrarians", null);
            if (output == null)
                return new Librarian[] { };
            return output.ToArray();
        }

        public static string GetPatronType(long patronId)
        {
            return DatabaseHelper.Query<string>("dbo.spPatrons_GetType @PatronId", new { PatronId = patronId }).FirstOrDefault();
        }

        public static Patron[] GetQueueToDocument(long documentId)
        {
            var output = DatabaseHelper.Query<Patron>("dbo.spQueue_GetQueueByDocumentId @DocumentId", new { DocumentId = documentId });
            if (output == null)
                return new Patron[] { };
            return output.ToArray();
        }

        public static void DeleteQueueToDocument(long documentId)
        {
            DatabaseHelper.Execute("dbo.spQueue_DeleteQueueByDocumentId @DocumentId", new { DocumentId = documentId });
        }

        public static void AddToQueue(long documentId, long patronId)
        {
            DatabaseHelper.Execute("dbo.spQueue_AddPatron @DocumentId, @PatronId", new { DocumentId = documentId, PatronId = patronId });
        }

        public static Patron[] GetPatronsCheckedByDocumentId(long documentId)
        {
            var output = DatabaseHelper.Query<Patron>("dbo.spCopies_GetPatronsByDocumentId @DocumentId", new { DocumentId = documentId });
            if (output == null)
                return new Patron[] { };
            return output.ToArray();
        }

        public static Fine[] GetFinesByPatronId(long patronId)
        {
            CopyWithPrice[] copies = new CopyWithPrice[] { };
            var output = DatabaseHelper.Query<CopyWithPrice>("dbo.spCopies_GetCheckedCopiesWithPriceByPatronId @PatronId",
                new
                {
                    PatronId = patronId
                });
            if (output != null)
                copies = output.ToArray();
            List<Fine> fines = new List<Fine>();
            foreach (CopyWithPrice copy in copies)
            {
                int fine = copy.CountFine();
                if (fine > 0)
                    fines.Add(new Fine {
                        DocumentId = copy.DocumentId,
                        FineAmount = fine,
                        PatronId = patronId });
            }
            return fines.ToArray();
        }

        public static void SetLibrarianAuthority(long librarianId, int authority)
        {
            DatabaseHelper.Execute("dbo.spLibrarians_ModifyAuthority @LibrarianId @Authority", new { LibrarianId = librarianId, Authority = authority });
        }
    }
}
