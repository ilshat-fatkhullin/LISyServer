using LISY.DataManagers;
using LISY.Entities.Documents;
using LISY.Entities.Requests;
using LISY.Entities.Requests.Librarian.Delete;
using LISY.Entities.Requests.Librarian.Post;
using LISY.Entities.Requests.Librarian.Put;
using LISY.Entities.Users;
using LISY.Entities.Users.Patrons;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    [Produces("application/json")]
    [Route("api/Librarian")]
    public class LibrarianController : Controller
    {        
        [Route("add_article")]
        [HttpPost]
        public long AddArticle([FromBody]Article article)
        {
            return DocumentsDataManager.AddArticle(article);
        }

        [Route("add_av_material")]
        [HttpPost]
        public long AddAVMaterial([FromBody]AVMaterial avMaterial)
        {
            return DocumentsDataManager.AddAVMaterial(avMaterial);
        }

        [Route("add_book")]
        [HttpPost]
        public long AddBook([FromBody]Book book)
        {
            return DocumentsDataManager.AddBook(book);
        }

        [Route("add_inner_material")]
        [HttpPost]
        public long AddInnerMaterial([FromBody]InnerMaterial innerMaterial)
        {
            return DocumentsDataManager.AddInnerMaterial(innerMaterial);
        }

        [Route("add_journal")]
        [HttpPost]
        public long AddJournal([FromBody]Journal journal)
        {
            return DocumentsDataManager.AddJournal(journal);
        }        
        
        [Route("edit_article")]
        [HttpPut]
        public void EditArticle([FromBody]Article article)
        {
            DocumentsDataManager.EditArticle(article);
        }

        [Route("edit_av_material")]
        [HttpPut]
        public void EditAVMaterial([FromBody]AVMaterial avMaterial)
        {
            DocumentsDataManager.EditAVMaterial(avMaterial);
        }

        [Route("edit_book")]
        [HttpPut]
        public void EditBook([FromBody]Book book)
        {
            DocumentsDataManager.EditBook(book);
        }

        [Route("edit_inner_material")]
        [HttpPut]
        public void EditInnerMaterial([FromBody]InnerMaterial innerMaterial)
        {
            DocumentsDataManager.EditInnerMaterial(innerMaterial);
        }

        [Route("edit_journal")]
        [HttpPost]
        public void EditJournal([FromBody]Journal journal)
        {
            DocumentsDataManager.EditJournal(journal);
        }        

        [Route("delete_document")]
        [HttpDelete]
        public void DeleteDocument([FromBody]DeleteDocumentRequest request)
        {
            DocumentsDataManager.DeleteDocument(request.Id);
        }

        [Route("return_document")]
        [HttpPut]
        public void ReturnDocument([FromBody]ReturnDocumentRequest request)
        {
            DocumentsDataManager.ReturnDocument(request.DocumentId, request.UserId);
        }        

        [Route("add_librarian")]
        [HttpPost]
        public bool AddLibrarian([FromBody]AddLibrarianRequest request)
        {
            return UsersDataManager.AddLibrarian(request.Librarian, request.Login, request.Password);
        }

        [Route("add_faculty")]
        [HttpPost]
        public bool AddFaculty([FromBody]AddFacultyRequest request)
        {
            return UsersDataManager.AddFaculty(request.Faculty, request.Login, request.Password);
        }

        [Route("add_student")]
        [HttpPost]
        public bool AddStudent([FromBody]AddStudentRequest request)
        {
            return UsersDataManager.AddStudent(request.Student, request.Login, request.Password);
        }

        [Route("add_guest")]
        [HttpPost]
        public bool AddGuest([FromBody]AddGuestRequest request)
        {
            return UsersDataManager.AddGuest(request.Guest, request.Login, request.Password);
        }             

        [Route("edit_librarian")]
        [HttpPut]
        public void EditLibrarian([FromBody]Librarian librarian)
        {
            UsersDataManager.EditLibrarian(librarian);
        }

        [Route("edit_faculty")]
        [HttpPut]
        public void EditFaculty([FromBody]Faculty faculty)
        {
            UsersDataManager.EditFaculty(faculty);
        }

        [Route("edit_student")]
        [HttpPut]
        public void EditStudent([FromBody]Student student)
        {
            UsersDataManager.EditStudent(student);
        }

        [Route("edit_guest")]
        [HttpPut]
        public void EditGuest([FromBody]Guest guest)
        {
            UsersDataManager.EditGuest(guest);
        }        
        
        [Route("get_user")]
        [HttpGet]
        public User GetUserById(long userId)
        {
            return UsersDataManager.GetUserById(userId);
        }

        [Route("get_patron")]
        [HttpGet]
        public Patron GetPatronById(long patronId)
        {
            return UsersDataManager.GetPatronById(patronId);
        }        

        [Route("delete_user")]
        [HttpDelete]
        public void DeleteUser([FromBody]DeleteUserRequest request)
        {
            UsersDataManager.DeleteUser(request.UserId);
        }

        [Route("get_all_users")]
        [HttpGet]
        public User[] GetAllUsersList()
        {
            return UsersDataManager.GetUsersList();
        }        
        
        [Route("add_copies")]
        [HttpPost]
        public void AddCopies([FromBody]AddCopiesRequest request)
        {
            DocumentsDataManager.AddCopies(request.N, request.Copy);
        }

        [Route("delete_copies")]
        [HttpDelete]
        public void DeleteCopy([FromBody]DeleteDocumentRequest request)
        {
            DocumentsDataManager.DeleteCopy(request.Id);
        }

        [Route("get_checked_copies")]
        [HttpGet]
        public Copy[] GetCheckedCopiesList()
        {
            return DocumentsDataManager.GetCheckedCopiesList();
        }

        [Route("get_all_copies")]
        [HttpGet]
        public Copy[] GetAllCopiesList()
        {
            return DocumentsDataManager.GetAllCopiesList();
        }

        [Route("get_copies_number")]
        [HttpGet]
        public int GetNumberOfCopies()
        {
            return DocumentsDataManager.GetNumberOfCopies();
        }        

        [Route("clear_all")]
        [HttpDelete]
        public void ClearAll()
        {
            DatabaseDataManager.ClearAll();
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

        [Route("get_queue_to_document")]
        [HttpGet]
        public Patron[] GetQueueToDocument(long documentId)
        {
            return UsersDataManager.GetQueueToDocument(documentId);
        }

        [Route("delete_queue_to_document")]
        [HttpDelete]
        public void DeleteQueueToDocument(DeleteQueueToDocumentRequest request)
        {
            UsersDataManager.DeleteQueueToDocument(request.DocumentId);
        }
    }
}