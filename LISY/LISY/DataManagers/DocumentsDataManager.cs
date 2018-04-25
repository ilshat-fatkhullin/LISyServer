using LISY.Entities.Documents;
using LISY.Entities.Notifications;
using LISY.Entities.Users;
using LISY.Helpers;
using System;
using System.Linq;

namespace LISY.DataManagers
{
    /// <summary>
    /// Contatins all functions for document to work with database
    /// </summary>
    public static class DocumentsDataManager
    {
        /// <summary>
        /// Adds AV material to database
        /// </summary>
        /// <param name="avMaterial">Information about AV material</param>
        /// <returns>Document id</returns>
        public static long AddAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_AddAV @Title, @Authors, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        avMaterial);

            return GetDocumentId(avMaterial);
        }

        /// <summary>
        /// Adds book to database
        /// </summary>
        /// <param name="book">Information about book</param>
        /// <returns>Document id</returns>
        public static long AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_AddBook @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        book);

            return GetDocumentId(book);
        }

        /// <summary>
        /// Adds inner material to database
        /// </summary>
        /// <param name="innerMaterial">Information about inner material</param>
        /// <returns>Document id</returns>
        public static long AddInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spInnerMaterials_AddInnerMaterial @Title, @Authors, @Type, @Room, @Level, @KeyWords, @CoverURL",
                        innerMaterial);

            return GetDocumentId(innerMaterial);
        }

        /// <summary>
        /// Adds journal to database
        /// </summary>
        /// <param name="journal">Information about journal</param>
        /// <returns>Document id</returns>
        public static long AddJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournals_AddJournal @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        journal);

            return GetDocumentId(journal);
        }

        /// <summary>
        /// Adds article to database
        /// </summary>
        /// <param name="article">Information about article</param>
        /// <returns>Document id</returns>
        public static long AddArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournalArticles_AddJournalArticle @Title, @Authors, @JournalId, @KeyWords, @CoverURL",
                        article);

            return GetDocumentId(article);
        }

        /// <summary>
        /// Edits av material in database
        /// </summary>
        /// <param name="avMaterial">Information about edited av material</param>
        public static void EditAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_ModifyAV @Id, @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);
        }

        /// <summary>
        /// Edits book in database
        /// </summary>
        /// <param name="book">Information about edited book</param>
        public static void EditBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_ModifyBook @Id, @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
                        book);
        }

        /// <summary>
        /// Edits inner material in database
        /// </summary>
        /// <param name="innerMaterial">Information about edited inner material</param>
        public static void EditInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spInnerMaterials_ModifyInnerMaterial @Id, @Title, @Authors, @KeyWords",
                        innerMaterial);
        }

        /// <summary>
        /// Edits journal in database
        /// </summary>
        /// <param name="journal">Information about edited journal</param>
        public static void EditJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournals_ModifyJournal @Id, @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
                        journal);
        }

        /// <summary>
        /// Edits article in database
        /// </summary>
        /// <param name="article">Information about edited article</param>
        public static void EditArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournalArticles_ModifyJournalArticle @Id, @Title, @Authors, @KeyWords",
                        article);
        }

        /// <summary>
        /// Deletes document from database
        /// </summary>
        /// <param name="id">Id of document that will be deleted</param>
        public static void DeleteDocument(long id)
        {
            DatabaseHelper.Execute("dbo.spDocuments_DeleteDocument @Id", new { Id = id });
        }

        /// <summary>
        /// Check out document in database
        /// </summary>
        /// <param name="documentId">Id of document that will be checked out</param>
        /// <param name="patronId">Id of patron that checks out the document</param>
        public static void CheckOutDocument(long documentId, long patronId)
        {
            if (!IsAvailable(documentId, patronId))
                return;

            string patronType = UsersDataManager.GetPatronType(patronId);

            Takable takable = GetTakableById(documentId);

            long returningDate = takable.EvaluateReturnDate(DateManager.GetLong(DateTime.Today), patronType);

            long availableCopyId = DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentId, UserId = patronId }).FirstOrDefault();

            DatabaseHelper.Execute("dbo.spCopies_takeCopyWithReturningDate @CopyId, @UserId, @ReturningDate", new { CopyId = availableCopyId, UserId = patronId, ReturningDate = returningDate });
        }

        /// <summary>
        /// Returns document in the database
        /// </summary>
        /// <param name="documentId">Id of document that will be returned</param>
        /// <param name="patronId">Id of patron that returns the document</param>
        public static void ReturnDocument(long documentId, long patronId)
        {
            DatabaseHelper.Execute("dbo.spCopies_ReturnDocument @DocumentId, @UserId", new { DocumentId = documentId, UserId = patronId });
            Patron[] patrons = UsersDataManager.GetQueueToDocument(documentId);
            if (patrons.Length != 0)
            {
                Takable takable = GetTakableById(documentId);
                NotificationsDataManager.AddNotification(new Notification() {
                    PatronId = patrons[0].CardNumber,
                    Message =  takable.Title + " now waiting for you." });
                DatabaseHelper.Execute("dbo.spQueue_RemovePatronByDocumentId @DocumentId, @PatronId", new { DocumentId = documentId, PatronId = patronId });
            }
        }

        /// <summary>
        /// Checks can current document be checked out by current patron
        /// </summary>
        /// <param name="documentID">Id of checking document</param>
        /// <param name="patronID">Id of checking patron</param>
        /// <returns></returns>
        public static bool IsAvailable(long documentID, long patronID)
        {
            return DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentID, UserId = patronID }).ToList().Count != 0;
        }

        /// <summary>
        /// Gets type of document with given document id
        /// </summary>
        /// <param name="documentId">Given document id</param>
        /// <returns></returns>
        public static string GetType(long documentId)
        {
            return DatabaseHelper.Query<string>("dbo.spDocuments_GetType @DocumentId", new { DocumentId = documentId }).FirstOrDefault();
        }

        /// <summary>
        /// Gets copy id by document id, room and level of given copy
        /// </summary>
        /// <param name="copy">Given copy</param>
        /// <returns>Copy id</returns>
        public static int GetCopyId(Copy copy)
        {
            var output = DatabaseHelper.Query<int>("dbo.spCopies_GetCopyId @DocId, @Room, @Level", new { DocId = copy.DocumentId, copy.Room, copy.Level }).ToList();
            if (output.Count() > 0)
            {
                return output[0];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Deletes copy by given copy id
        /// </summary>
        /// <param name="id">Given copy id</param>
        public static void DeleteCopy(long id)
        {
            DatabaseHelper.Execute("dbo.spCopies_DeleteCopy @CopyId", new { CopyId = id });
        }

        /// <summary>
        /// Gets number of documents in the database
        /// </summary>
        /// <returns>Number of documents in the database</returns>
        public static int GetNumberOfDocuments()
        {
            return DatabaseHelper.Query<int>("dbo.spDocuments_GetNumberOfDocuments", null).FirstOrDefault();
        }

        /// <summary>
        /// Gets number of copies in the database
        /// </summary>
        /// <returns>Number of copies in the database</returns>
        public static int GetNumberOfCopies()
        {
            return DatabaseHelper.Query<int>("dbo.spCopies_GetNumberOfCopies", null).FirstOrDefault();
        }

        /// <summary>
        /// Adds <code>number</code> amount of copies in the database.
        /// </summary>
        /// <param name="number">Number of copies that will be added</param>
        /// <param name="copy">Information of copies that will be added</param>
        public static void AddCopies(int number, Copy copy)
        {
            DatabaseHelper.Execute("dbo.spCopies_AddCopy @N, @BookId, @Room, @Level", new { N = number, BookId = copy.DocumentId, Room = copy.Room, Level = copy.Level });
        }

        /// <summary>
        /// Gets list of all copies in the database
        /// </summary>
        /// <returns>Array of all copies in the database</returns>
        public static Copy[] GetAllCopiesList()
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetAll", null);
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Get cheked out copies list
        /// </summary>
        /// <returns>Array of all checked out copies</returns>
        public static Copy[] GetCheckedOutCopiesList()
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetChecked", null);
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all copies which are checked out by patron with given patron id
        /// </summary>
        /// <param name="patronId">Given patron id</param>
        /// <returns></returns>
        public static Copy[] GetCopiesCheckedOutByPatron(long patronId)
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetCheckedByUser @UserId", new { UserId = patronId });
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all av materials in database
        /// </summary>
        /// <returns>Array of all av materials in database</returns>
        public static AVMaterial[] GetAllAVMaterialsList()
        {
            var output = DatabaseHelper.Query<AVMaterial>("dbo.spAudioVideos_GetAll", null);
            if (output == null)
                return new AVMaterial[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all books in the database
        /// </summary>
        /// <returns>Array of all books in database</returns>
        public static Book[] GetAllBooksList()
        {
            var output = DatabaseHelper.Query<Book>("dbo.spBooks_GetAll", null);
            if (output == null)
                return new Book[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all inner materials in database
        /// </summary>
        /// <returns>Array of all inner materials in database</returns>
        public static InnerMaterial[] GetAllInnerMaterialsList()
        {
            var output = DatabaseHelper.Query<InnerMaterial>("dbo.spInnerMaterials_GetAll", null);
            if (output == null)
                return new InnerMaterial[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all journals in database
        /// </summary>
        /// <returns>Array of all journals in database</returns>
        public static Journal[] GetAllJournalsList()
        {
            var output = DatabaseHelper.Query<Journal>("dbo.spJournals_GetAll", null);
            if (output == null)
                return new Journal[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets list of all articles in database
        /// </summary>
        /// <returns>Array of all articles in database</returns>
        public static Article[] GetAllArticlesList()
        {
            var output = DatabaseHelper.Query<Article>("dbo.spJournalArticles_GetAll", null);
            if (output == null)
                return new Article[] { };
            return output.ToArray();
        }

        /// <summary>
        /// Gets document id by content of given document
        /// </summary>
        /// <param name="document">Given document</param>
        /// <returns>Document id</returns>
        public static long GetDocumentId(Document document)
        {
            return DatabaseHelper.Query<Document>("dbo.spDocuments_GetDocumentId @Title, @Authors, @KeyWords",
                new { Title = document.Title, Authors = document.Authors, KeyWords = document.KeyWords }).FirstOrDefault().Id;
        }

        /// <summary>
        /// Sets state of outstanding request to given document
        /// </summary>
        /// <param name="state">State of outstanding request</param>
        /// <param name="documentId">Given document id</param>
        public static void SetOutstanding(bool state, long documentId)
        {
            if (state)
            {
                string title = GetTakableById(documentId).Title;
                string message = title + " is not available now.";
                foreach (Patron p in UsersDataManager.GetQueueToDocument(documentId))
                {
                    NotificationsDataManager.AddNotification(new Notification() { PatronId = p.CardNumber, Message = message});
                }
                message = "You have to return document " + title + ".";
                foreach (Patron p in UsersDataManager.GetPatronsCheckedByDocumentId(documentId))
                {
                    NotificationsDataManager.AddNotification(new Notification() { PatronId = p.CardNumber, Message = message });
                }
            }
            DatabaseHelper.Execute("dbo.spTakable_SetOutstanding @State, @DocumentId", new { State = state, DocumentId = documentId });
        }

        /// <summary>
        /// Renews copy by given document id and patron id
        /// </summary>
        /// <param name="documentId">Given document id</param>
        /// <param name="patronId">Given patron id</param>
        public static void RenewCopy(long documentId, long patronId)
        {
            Copy[] copies = GetCopiesCheckedOutByPatron(patronId);
            Copy copy = null;
            foreach (Copy c in copies)
            {
                if (c.DocumentId == documentId && c.PatronId == patronId)
                {
                    copy = c;
                }
            }
            if (copy == null)
                return;
            string patronType = UsersDataManager.GetPatronType(patronId);
            if (copy.IsRenewed && patronType != "Guest")
                return;
            Takable takable = GetTakableById(documentId);
            if (takable.IsOutstanding)
                return;
            long returningDate = takable.EvaluateReturnDate(copy.ReturningDate, patronType);
            DatabaseHelper.Execute("dbo.spCopies_RenewDocument @DocumentId, @PatronId, @ReturningDate", new {
                DocumentId = documentId,
                PatronId = patronId,
                ReturningDate = returningDate});
        }

        /// <summary>
        /// Gets takable by given takable id
        /// </summary>
        /// <param name="takableId">Given takable id</param>
        /// <returns>Information about takable</returns>
        public static Takable GetTakableById(long takableId)
        {
            string documentType = GetType(takableId);
            Takable takable = DatabaseHelper.Query<Book>("dbo.spTakable_GetAllById @DocumentId", new { DocumentId = takableId }).ToArray()[0];            
            return takable;
        }

        /// <summary>
        /// Gets takables checked out by patron with given patron id
        /// </summary>
        /// <param name="patronId">Given patron id</param>
        /// <returns>Takables checked out by patron</returns>
        public static Takable[] GetTakableCheckedOutByPatron(long patronId)
        {
            var output = DatabaseHelper.Query<AVMaterial>("dbo.spTakable_GetCheckedByPatronId @PatronId",
                new {
                    PatronId = patronId
                });
            if (output == null)
                return new AVMaterial[] { };
            return output.ToArray();
        }
    }
}