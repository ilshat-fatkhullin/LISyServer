using LISY.DataManagers;
using LISY.Entities.Documents;
using LISY.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Librarian")]
    public class LibrarianController : Controller
    {
        [Route("add_article")]
        [HttpPost]
        public long AddArticle(Article article)
        {
            return DocumentsDataManager.AddArticle(article);
        }

        [Route("edit_article")]
        [HttpPut]
        public void EditArticle(Article article)
        {
            DocumentsDataManager.EditArticle(article);
        }

        [Route("add_av_material")]
        [HttpPost]
        public long AddAVMaterial(AVMaterial avMaterial)
        {
            return DocumentsDataManager.AddAVMaterial(avMaterial);
        }

        [Route("edit_av_material")]
        [HttpPut]
        public void EditAVMaterial(AVMaterial avMaterial)
        {
            DocumentsDataManager.EditAVMaterial(avMaterial);
        }

        [Route("add_book")]
        [HttpPost]
        public long AddBook(Book book)
        {
            return DocumentsDataManager.AddBook(book);
        }

        [Route("edit_book")]
        [HttpPut]
        public void EditBook(Book book)
        {
            DocumentsDataManager.EditBook(book);
        }

        [Route("add_inner_material")]
        [HttpPost]
        public long AddInnerMaterial(InnerMaterial innerMaterial)
        {
            return DocumentsDataManager.AddInnerMaterial(innerMaterial);
        }

        [Route("edit_inner_material")]
        [HttpPut]
        public void EditInnerMaterial(InnerMaterial innerMaterial)
        {
            DocumentsDataManager.EditInnerMaterial(innerMaterial);
        }

        [Route("add_journal")]
        [HttpPost]
        public long AddJournal(Journal journal)
        {
            return DocumentsDataManager.AddJournal(journal);
        }

        [Route("edit_journal")]
        [HttpPost]
        public void EditJournal(Journal journal)
        {
            DocumentsDataManager.EditJournal(journal);
        }

        [Route("delete_document")]
        [HttpDelete]
        public void DeleteDocument(long id)
        {
            DocumentsDataManager.DeleteDocument(id);
        }

        [Route("return_document")]
        [HttpPut]
        public void ReturnDocument(long documentId, long userId)
        {
            DocumentsDataManager.ReturnDocument(documentId, userId);
        }

        [Route("add_user")]
        [HttpPost]
        public bool AddUser(User user, string login, string password)
        {
            return UsersDataManager.AddUser(user, login, password);
        }

        [Route("delete_user")]
        [HttpDelete]
        public void DeleteUser(User user)
        {
            UsersDataManager.DeleteUser(user);
        }

        [Route("edit_user")]
        [HttpPut]
        public void EditUser(User newUser)
        {
            UsersDataManager.EditUser(newUser);
        }

        [Route("add_copies")]
        [HttpPost]
        public void AddCopies(int n, Copy copy)
        {
            DocumentsDataManager.AddCopies(n, copy);
        }

        [Route("delete_copies")]
        [HttpDelete]
        public void DeleteCopy(Copy copy)
        {
            DocumentsDataManager.DeleteCopy(copy);
        }

        [Route("delete_copy_by_id")]
        [HttpDelete]
        public void DeleteCopyByDocId(Copy copy)
        {
            DocumentsDataManager.DeleteCopyByDocumentId(copy);
        }

        [Route("get_all_copies")]
        [HttpGet]
        public Copy[] GetAllCopiesList()
        {
            return DocumentsDataManager.GetAllCopiesList();
        }

        [Route("clear_all")]
        [HttpDelete]
        public void ClearAll()
        {
            DatabaseDataManager.ClearAll();
        }

        [Route("get_checked_copies")]
        [HttpGet]
        public Copy[] GetCheckedCopiesList()
        {
            return DocumentsDataManager.GetCheckedCopiesList();
        }

        [Route("get_all_users")]
        [HttpGet]
        public User[] GetAllUsersList()
        {
            return UsersDataManager.GetUsersList();
        }

        [Route("get_all_av_materials")]
        [HttpGet]
        public AVMaterial[] GetAllAVMaterialsList()
        {
            return DocumentsDataManager.GetAllAVMaterialsList();
        }

        [Route("get_all_books")]
        [HttpGet]
        public Book[] GetAllBooksList()
        {
            return DocumentsDataManager.GetAllBooksList();
        }

        [Route("get_documents_number")]
        [HttpGet]
        public int GetNumberOfDocuments()
        {
            return DocumentsDataManager.GetNumberOfDocuments();
        }

        [Route("get_users_number")]
        [HttpGet]
        public int GetNumberOfUsers()
        {
            return UsersDataManager.GetNumberOfUsers();
        }

        [Route("get_copies_number")]
        [HttpGet]
        public int GetNumberOfCopies()
        {
            return DocumentsDataManager.GetNumberOfCopies();
        }

        [Route("get_all_inner_materials")]
        [HttpGet]
        public InnerMaterial[] GetAllInnerMaterialsList()
        {
            return DocumentsDataManager.GetAllInnerMaterialsList();
        }

        [Route("get_all_journals")]
        [HttpGet]
        public Journal[] GetAllJournalsList()
        {
            return DocumentsDataManager.GetAllJournalsList();
        }

        [Route("get_all_articles")]
        [HttpGet]
        public Article[] GetAllArticlesList()
        {
            return DocumentsDataManager.GetAllArticlesList();
        }

        [Route("get_user")]
        [HttpGet]
        public User GetUserById(long userId)
        {
            return CredentialsDataManager.GetUserByID(userId);
        }

        [Route("get_copies_checked_by_user")]
        [HttpGet]
        public Copy[] GetCheckedByUserCopiesList(long userId)
        {
            return DocumentsDataManager.GetCheckedByUserCopiesList(userId);
        }

        [Route("is_available")]
        [HttpGet]
        public bool IsAvailable(long documentId, long patronId)
        {
            return DocumentsDataManager.IsAvailable(documentId, patronId);
        }        
    }
}