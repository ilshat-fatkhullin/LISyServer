using LISY.DataManagers;
using LISY.Entities.Documents;
using LISY.Entities.Fines;
using LISY.Entities.Requests;
using LISY.Entities.Requests.Librarian.Delete;
using LISY.Entities.Requests.Librarian.Post;
using LISY.Entities.Requests.Librarian.Put;
using LISY.Entities.Users;
using LISY.Entities.Users.Patrons;
using Microsoft.AspNetCore.Mvc;

namespace LISY.Controllers
{
    /// <summary>
    /// Implements all HTTP requests to librarian
    /// </summary>
    [Produces("application/json")]
    [Route("api/Librarian")]
    public class LibrarianController : Controller
    {
        /// <summary>
        /// Adds given article to database
        /// </summary>
        /// <param name="article">Given article</param>
        /// <returns>Id of added article</returns>
        [Route("add_article")]
        [HttpPost]
        public long AddArticle([FromBody]ArticleRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added article " + request.Article.Title);
            return DocumentsDataManager.AddArticle(request.Article);
        }

        /// <summary>
        /// Adds given AV material to database
        /// </summary>
        /// <param name="avMaterial">Given AV material</param>
        /// <returns>Id of added AV material</returns>
        [Route("add_av_material")]
        [HttpPost]
        public long AddAVMaterial([FromBody]AVMaterialRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added av material " + request.AVMaterial.Title);
            return DocumentsDataManager.AddAVMaterial(request.AVMaterial);
        }

        /// <summary>
        /// Adds given bool to database
        /// </summary>
        /// <param name="book">Given book</param>
        /// <returns>Id of added book</returns>
        [Route("add_book")]
        [HttpPost]
        public long AddBook([FromBody]BookRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added book " + request.Book.Title);
            return DocumentsDataManager.AddBook(request.Book);
        }

        /// <summary>
        /// Adds given inner material to database
        /// </summary>
        /// <param name="innerMaterial">Given inner material</param>
        /// <returns>Id of added inner material</returns>
        [Route("add_inner_material")]
        [HttpPost]
        public long AddInnerMaterial([FromBody]InnerMaterialRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added inner material " + request.InnerMaterial.Title);
            return DocumentsDataManager.AddInnerMaterial(request.InnerMaterial);
        }

        /// <summary>
        /// Adds given journal to database
        /// </summary>
        /// <param name="journal">Given journal</param>
        /// <returns>Id of added journal</returns>
        [Route("add_journal")]
        [HttpPost]
        public long AddJournal([FromBody]JournalRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added journal " + request.Journal.Title);
            return DocumentsDataManager.AddJournal(request.Journal);
        }        
        
        /// <summary>
        /// Edits given article in database
        /// </summary>
        /// <param name="article">Given article</param>
        [Route("edit_article")]
        [HttpPut]
        public void EditArticle([FromBody]ArticleRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited article " + request.Article.Title);
            DocumentsDataManager.EditArticle(request.Article);
        }

        /// <summary>
        /// Edits given AV material in database
        /// </summary>
        /// <param name="avMaterial">Given AV material</param>
        [Route("edit_av_material")]
        [HttpPut]
        public void EditAVMaterial([FromBody]AVMaterialRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited av material " + request.AVMaterial.Title);
            DocumentsDataManager.EditAVMaterial(request.AVMaterial);
        }

        /// <summary>
        /// Edits given book in database
        /// </summary>
        /// <param name="book">Given book</param>
        [Route("edit_book")]
        [HttpPut]
        public void EditBook([FromBody]BookRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited book " + request.Book.Title);
            DocumentsDataManager.EditBook(request.Book);
        }

        /// <summary>
        /// Edits given inner material in database
        /// </summary>
        /// <param name="innerMaterial">Given inner material</param>
        [Route("edit_inner_material")]
        [HttpPut]
        public void EditInnerMaterial([FromBody]InnerMaterialRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited inner material " + request.InnerMaterial.Title);
            DocumentsDataManager.EditInnerMaterial(request.InnerMaterial);
        }

        /// <summary>
        /// Edits given journal in database
        /// </summary>
        /// <param name="journal">Given journal</param>
        [Route("edit_journal")]
        [HttpPost]
        public void EditJournal([FromBody]JournalRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited journal " + request.Journal.Title);
            DocumentsDataManager.EditJournal(request.Journal);
        }        

        /// <summary>
        /// Deletes given document from database
        /// </summary>
        /// <param name="request">Document id</param>
        [Route("delete_document")]
        [HttpDelete]
        public void DeleteDocument([FromBody]DeleteDocumentRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "deleted document " + request.Id);
            DocumentsDataManager.DeleteDocument(request.Id);
        }

        /// <summary>
        /// Returns given document from patron to library
        /// </summary>
        /// <param name="request">Document and patron ids</param>
        [Route("return_document")]
        [HttpPut]
        public void ReturnDocument([FromBody]ReturnDocumentRequest request)
        {
            LogsDataManager.SendLog(
                request.UserId,
                "Patron",
                "returned document " + request.DocumentId);
            DocumentsDataManager.ReturnDocument(request.DocumentId, request.UserId);
        }        

        /// <summary>
        /// Adds given librarian to database
        /// </summary>
        /// <param name="request">Librarian and its credentials</param>
        /// <returns>Is added successful</returns>
        [Route("add_librarian")]
        [HttpPost]
        public bool AddLibrarian([FromBody]AddLibrarianRequest request)
        {
            LogsDataManager.SendLog(
                1,
                "Admin",
                "edited article " + request.Librarian.FirstName);
            return UsersDataManager.AddLibrarian(request.Librarian, request.Login, request.Password);
        }

        /// <summary>
        /// Adds given faculty to database
        /// </summary>
        /// <param name="request">Faculty and its credentials</param>
        /// <returns>Is added successful</returns>
        [Route("add_faculty")]
        [HttpPost]
        public bool AddFaculty([FromBody]AddFacultyRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added faculty " + request.Faculty.FirstName);
            return UsersDataManager.AddFaculty(request.Faculty, request.Login, request.Password);
        }

        /// <summary>
        /// Adds given student to database
        /// </summary>
        /// <param name="request">Student and its credentials</param>
        /// <returns>Is added successful</returns>
        [Route("add_student")]
        [HttpPost]
        public bool AddStudent([FromBody]AddStudentRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added student " + request.Student.FirstName);
            return UsersDataManager.AddStudent(request.Student, request.Login, request.Password);
        }

        /// <summary>
        /// Adds given guest to database
        /// </summary>
        /// <param name="request">Guest and its credentials</param>
        /// <returns>Is added successful</returns>
        [Route("add_guest")]
        [HttpPost]
        public bool AddGuest([FromBody]AddGuestRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added guest " + request.Guest.FirstName);
            return UsersDataManager.AddGuest(request.Guest, request.Login, request.Password);
        }             

        /// <summary>
        /// Edits given librarian in database
        /// </summary>
        /// <param name="librarian">Given librarian</param>
        [Route("edit_librarian")]
        [HttpPut]
        public void EditLibrarian([FromBody]Librarian librarian)
        {
            LogsDataManager.SendLog(
                1,
                "Admin",
                "edited faculty " + librarian.FirstName);
            UsersDataManager.EditLibrarian(librarian);
        }

        /// <summary>
        /// Edits given faculty in database
        /// </summary>
        /// <param name="faculty">Given faculty</param>
        [Route("edit_faculty")]
        [HttpPut]
        public void EditFaculty([FromBody]EditFacultyRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited faculty " + request.Faculty.FirstName);
            UsersDataManager.EditFaculty(request.Faculty);
        }

        /// <summary>
        /// Edits given student in database
        /// </summary>
        /// <param name="student">Given student</param>
        [Route("edit_student")]
        [HttpPut]
        public void EditStudent([FromBody]EditStudentRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited student " + request.Student.FirstName);            
            UsersDataManager.EditStudent(request.Student);
        }

        /// <summary>
        /// Edit given guest in database
        /// </summary>
        /// <param name="guest">Given guest</param>
        [Route("edit_guest")]
        [HttpPut]
        public void EditGuest([FromBody]EditGuestRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited guest " + request.Guest.FirstName);
            UsersDataManager.EditGuest(request.Guest);
        }

        /// <summary>
        /// Deletes user by given user id
        /// </summary>
        /// <param name="request">Given user id</param>
        [Route("delete_user")]
        [HttpDelete]
        public void DeleteUser([FromBody]DeleteUserRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "edited user " + request.UserId);
            UsersDataManager.DeleteUser(request.UserId);
        }        
        
        /// <summary>
        /// Adds given copy to database
        /// </summary>
        /// <param name="request">Given copy</param>
        [Route("add_copies")]
        [HttpPost]
        public void AddCopies([FromBody]AddCopiesRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "added " + request.N + " copies for document " + request.Copy.DocumentId);
            DocumentsDataManager.AddCopies(request.N, request.Copy);
        }

        /// <summary>
        /// Deletes given copy from database
        /// </summary>
        /// <param name="request">Given copy</param>
        [Route("delete_copies")]
        [HttpDelete]
        public void DeleteCopy([FromBody]DeleteDocumentRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "deleted document by id " + request.Id);
            DocumentsDataManager.DeleteCopy(request.Id);
        }

        /// <summary>
        /// Deletes queue to document
        /// </summary>
        /// <param name="request">Document id</param>
        [Route("delete_queue_to_document")]
        [HttpDelete]
        public void DeleteQueueToDocument([FromBody]DeleteQueueToDocumentRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "deleted queue to document with id " + request.DocumentId);
            UsersDataManager.DeleteQueueToDocument(request.DocumentId);
        }

        /// <summary>
        /// Sets state of outstanding request on given document
        /// </summary>
        /// <param name="request">Document id</param>
        [Route("set_outstanding")]
        [HttpPut]
        public void SetOutstanding([FromBody]MakeOutstandingRequest request)
        {
            LogsDataManager.SendLog(
                request.LibrarianId,
                "Librarian",
                "seted outstanding request to document with id " + request.DocumentId);
            DocumentsDataManager.SetOutstanding(request.State, request.DocumentId);
        }

        /// <summary>
        /// Gets user by given user id
        /// </summary>
        /// <param name="userId">Given user id</param>
        /// <returns>User</returns>
        [Route("get_user")]
        [HttpGet]
        public User GetUserById(long userId)
        {
            return UsersDataManager.GetUserById(userId);
        }

        /// <summary>
        /// Gets patron by given patron id
        /// </summary>
        /// <param name="patronId">Given patron id</param>
        /// <returns>Patron</returns>
        [Route("get_patron")]
        [HttpGet]
        public Patron GetPatronById(long patronId)
        {
            return UsersDataManager.GetPatronById(patronId);
        }

        /// <summary>
        /// Get takable by given takable id
        /// </summary>
        /// <param name="takableId">Given takable id</param>
        /// <returns>Takable</returns>
        [Route("get_takable")]
        [HttpGet]
        public Takable GetTakableById(long takableId)
        {
            return DocumentsDataManager.GetTakableById(takableId);
        }

        /// <summary>
        /// Gets list of all users
        /// </summary>
        /// <returns>List of all users</returns>
        [Route("get_all_users")]
        [HttpGet]
        public User[] GetAllUsersList()
        {
            return UsersDataManager.GetUsersList();
        }

        /// <summary>
        /// Gets checked out copies list
        /// </summary>
        /// <returns>List of checked copies</returns>
        [Route("get_checked_copies")]
        [HttpGet]
        public Copy[] GetCheckedOutCopiesList()
        {
            return DocumentsDataManager.GetCheckedOutCopiesList();
        }

        /// <summary>
        /// Gets list of all copies
        /// </summary>
        /// <returns>List of all copies</returns>
        [Route("get_all_copies")]
        [HttpGet]
        public Copy[] GetAllCopiesList()
        {
            return DocumentsDataManager.GetAllCopiesList();
        }

        /// <summary>
        /// Gets number of copies
        /// </summary>
        /// <returns>Number of copies</returns>
        [Route("get_copies_number")]
        [HttpGet]
        public int GetNumberOfCopies()
        {
            return DocumentsDataManager.GetNumberOfCopies();
        }        

        /// <summary>
        /// Clears the database
        /// </summary>
        [Route("clear_all")]
        [HttpDelete]
        public void ClearAll()
        {
            DatabaseDataManager.ClearAll();
        }        

        /// <summary>
        /// Gets list of all av materials
        /// </summary>
        /// <returns>List of all av materials</returns>
        [Route("get_all_av_materials")]
        [HttpGet]
        public AVMaterial[] GetAllAVMaterialsList()
        {
            return DocumentsDataManager.GetAllAVMaterialsList();
        }

        /// <summary>
        /// Gets list of all books
        /// </summary>
        /// <returns>List of all books</returns>
        [Route("get_all_books")]
        [HttpGet]
        public Book[] GetAllBooksList()
        {
            return DocumentsDataManager.GetAllBooksList();
        }

        /// <summary>
        /// Gets number of documents
        /// </summary>
        /// <returns>Number of documents</returns>
        [Route("get_documents_number")]
        [HttpGet]
        public int GetNumberOfDocuments()
        {
            return DocumentsDataManager.GetNumberOfDocuments();
        }

        /// <summary>
        /// Gets number of users
        /// </summary>
        /// <returns>Number of users</returns>
        [Route("get_users_number")]
        [HttpGet]
        public int GetNumberOfUsers()
        {
            return UsersDataManager.GetNumberOfUsers();
        }        

        /// <summary>
        /// Gets list of all inner materials
        /// </summary>
        /// <returns>List of all inner materials</returns>
        [Route("get_all_inner_materials")]
        [HttpGet]
        public InnerMaterial[] GetAllInnerMaterialsList()
        {
            return DocumentsDataManager.GetAllInnerMaterialsList();
        }

        /// <summary>
        /// Gets list of all journals
        /// </summary>
        /// <returns>List of all journals</returns>
        [Route("get_all_journals")]
        [HttpGet]
        public Journal[] GetAllJournalsList()
        {
            return DocumentsDataManager.GetAllJournalsList();
        }

        /// <summary>
        /// Gets list of all articles
        /// </summary>
        /// <returns>List of all articles</returns>
        [Route("get_all_articles")]
        [HttpGet]
        public Article[] GetAllArticlesList()
        {
            return DocumentsDataManager.GetAllArticlesList();
        }

        /// <summary>
        /// Gets checked out by patron copies list
        /// </summary>
        /// <param name="userId">Patron id</param>
        /// <returns>Checked out by patron copies list</returns>
        [Route("get_copies_checked_by_user")]
        [HttpGet]
        public Copy[] GetCheckedOutByPatronCopiesList(long userId)
        {
            return DocumentsDataManager.GetCopiesCheckedOutByPatron(userId);
        }

        /// <summary>
        /// Checks can current document be checked out by current patron
        /// </summary>
        /// <param name="documentId">Document id</param>
        /// <param name="patronId">Patron id</param>
        /// <returns></returns>
        [Route("is_available")]
        [HttpGet]
        public bool IsAvailable(long documentId, long patronId)
        {
            return DocumentsDataManager.IsAvailable(documentId, patronId);
        }

        /// <summary>
        /// Gets queue to document
        /// </summary>
        /// <param name="documentId">Document id</param>
        /// <returns>List of patrons</returns>
        [Route("get_queue_to_document")]
        [HttpGet]
        public Patron[] GetQueueToDocument(long documentId)
        {
            return UsersDataManager.GetQueueToDocument(documentId);
        }

        /// <summary>
        /// Get list of fines of patron with given id
        /// </summary>
        /// <param name="patronId">Patron id</param>
        /// <returns>List of fines</returns>
        [Route("get_fines_by_patron")]
        [HttpGet]
        public Fine[] GetFinesByPatronId(long patronId)
        {
            return UsersDataManager.GetFinesByPatronId(patronId);
        }
    }
}