using LISY.DataManagers;
using LISY.Entities.Documents;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Librarian")]
    public class LibrarianController : Controller
    {
        [Route("add_article")]
        [HttpPost]
        public static void AddArticle(Article article)
        {
            DocumentsDataManager.AddArticle(article);
        }

        [Route("edit_article")]
        [HttpPut]
        public static void EditArticle(Article article)
        {
            DocumentsDataManager.EditArticle(article);
        }

        [Route("add_av_material")]
        [HttpPost]
        public static void AddAVMaterial(AVMaterial avMaterial)
        {
            DocumentsDataManager.AddAVMaterial(avMaterial);
        }

        [Route("edit_av_material")]
        [HttpPut]
        public static void EditAVMaterial(AVMaterial avMaterial)
        {
            DocumentsDataManager.EditAVMaterial(avMaterial);
        }

        [Route("add_book")]
        [HttpPost]
        public static void AddBook(Book book)
        {
            DocumentsDataManager.AddBook(book);
        }

        [Route("edit_book")]
        [HttpPut]
        public static void EditBook(Book book)
        {
            DocumentsDataManager.EditBook(book);
        }

        [Route("add_inner_material")]
        [HttpPost]
        public static void AddInnerMaterial(InnerMaterial innerMaterial)
        {
            DocumentsDataManager.AddInnerMaterial(innerMaterial);
        }

        [Route("edit_inner_material")]
        [HttpPut]
        public static void EditInnerMaterial(InnerMaterial innerMaterial)
        {
            DocumentsDataManager.EditInnerMaterial(innerMaterial);
        }

        [Route("add_journal")]
        [HttpPost]
        public static void AddJournal(Journal journal)
        {
            DocumentsDataManager.AddJournal(journal);
        }

        [Route("edit_journal")]
        [HttpPost]
        public static void EditJournal(Journal journal)
        {
            DocumentsDataManager.EditJournal(journal);
        }

        [Route("delete_document")]
        [HttpDelete]
        public static void DeleteDocument(int id)
        {
            DocumentsDataManager.DeleteDocument(id);
        }

        [Route("delete_document")]
        [HttpPost]
        public static void ReturnDocument(long documentId, long userId)
        {
            DocumentsDataManager.ReturnDocument(documentId, userId);
        }

        [Route("add_user")]
        [HttpPost]
        public static bool AddUser(User user, string login, string password)
        {
            return UsersDataManager.AddUser(user, login, password);
        }

        [Route("delete_user")]
        [HttpDelete]
        public static void DeleteUser(User user)
        {
            UsersDataManager.DeleteUser(user);
        }

        [Route("edit_user")]
        [HttpPut]
        public static void EditUser(User newUser)
        {
            UsersDataManager.EditUser(newUser);
        }

        [Route("add_copies")]
        [HttpPost]
        public static void AddCopies(int n, Copy copy)
        {
            DocumentsDataManager.AddCopies(n, copy);
        }

        [Route("delete_copies")]
        [HttpDelete]
        public static void DeleteCopy(Copy copy)
        {
            DocumentsDataManager.DeleteCopy(copy);
        }

        [Route("delete_copy_by_id")]
        [HttpDelete]
        public static void DeleteCopyByDocId(Copy copy)
        {
            DocumentsDataManager.DeleteCopyByDocumentId(copy);
        }

        [Route("get_all_copies")]
        [HttpGet]
        public static Copy[] GetAllCopiesList()
        {
            return DocumentsDataManager.GetAllCopiesList();
        }

        [Route("clear_all")]
        [HttpDelete]
        public static void ClearAll()
        {
            DatabaseDataManager.ClearAll();
        }

        [Route("get_checked_copies")]
        [HttpGet]
        public static Copy[] GetCheckedCopiesList()
        {
            return DocumentsDataManager.GetCheckedCopiesList();
        }

        [Route("get_all_users")]
        [HttpGet]
        public static User[] GetAllUsersList()
        {
            return UsersDataManager.GetUsersList();
        }

        [Route("get_all_av_materials")]
        [HttpGet]
        public static AVMaterial[] GetAllAVMaterialsList()
        {
            return DocumentsDataManager.GetAllAVMaterialsList();
        }

        [Route("get_all_books")]
        [HttpGet]
        public static Book[] GetAllBooksList()
        {
            return DocumentsDataManager.GetAllBooksList();
        }

        [Route("get_documents_number")]
        [HttpGet]
        public static int GetNumberOfDocuments()
        {
            return DocumentsDataManager.GetNumberOfDocuments();
        }

        [Route("get_users_number")]
        [HttpGet]
        public static int GetNumberOfUsers()
        {
            return UsersDataManager.GetNumberOfUsers();
        }

        [Route("get_copies_number")]
        [HttpGet]
        public static int GetNumberOfCopies()
        {
            return DocumentsDataManager.GetNumberOfCopies();
        }

        [Route("get_all_inner_materials")]
        [HttpGet]
        public static InnerMaterial[] GetAllInnerMaterialsList()
        {
            return DocumentsDataManager.GetAllInnerMaterialsList();
        }

        [Route("get_all_journals")]
        [HttpGet]
        public static Journal[] GetAllJournalsList()
        {
            return DocumentsDataManager.GetAllJournalsList();
        }

        [Route("get_all_articles")]
        [HttpGet]
        public static Article[] GetAllArticlesList()
        {
            return DocumentsDataManager.GetAllArticlesList();
        }

        [Route("get_user")]
        [HttpGet]
        public static User GetUserById(long userId)
        {
            return CredentialsDataManager.GetUserByID(userId);
        }

        [Route("get_copies_checked_by_user")]
        [HttpGet]
        public static Copy[] GetCheckedByUserCopiesList(long userId)
        {
            return DocumentsDataManager.GetCheckedByUserCopiesList(userId);
        }
    }
}