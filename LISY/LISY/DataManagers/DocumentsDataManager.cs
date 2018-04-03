using LISY.Entities.Documents;
using LISY.Entities.Notifications;
using LISY.Entities.Users;
using LISY.Helpers;
using System;
using System.Linq;

namespace LISY.DataManagers
{
    public static class DocumentsDataManager
    {
        public static long AddAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_AddAV @Title, @Authors, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        avMaterial);

            return GetDocumentId(avMaterial);
        }

        public static long AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_AddBook @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        book);

            return GetDocumentId(book);
        }

        public static long AddInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spInnerMaterials_AddInnerMaterial @Title, @Authors, @Type, @Room, @Level, @KeyWords, @CoverURL",
                        innerMaterial);

            return GetDocumentId(innerMaterial);
        }

        public static long AddJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournals_AddJournal @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price, @IsOutstanding",
                        journal);

            return GetDocumentId(journal);
        }

        public static long AddArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournalArticles_AddJournalArticle @Title, @Authors, @JournalId, @KeyWords, @CoverURL",
                        article);

            return GetDocumentId(article);
        }

        public static void EditAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_ModifyAV @Id, @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);
        }

        public static void EditBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_ModifyBook @Id, @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
                        book);
        }

        public static void EditInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spInnerMaterials_ModifyInnerMaterial @Id, @Title, @Authors, @KeyWords",
                        innerMaterial);
        }

        public static void EditJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournals_ModifyJournal @Id, @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
                        journal);
        }

        public static void EditArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournalArticles_ModifyJournalArticle @Id, @Title, @Authors, @KeyWords",
                        article);
        }

        public static void DeleteDocument(long id)
        {
            DatabaseHelper.Execute("dbo.spDocuments_DeleteDocument @Id", new { Id = id });
        }

        public static void CheckOutDocument(long documentId, long patronId)
        {
            if (!IsAvailable(documentId, patronId))
                return;

            string patronType = UsersDataManager.GetPatronType(patronId);

            Takable takable = GetTakableById(documentId);

            string returningDate = takable.EvaluateReturnDate(DateTime.Today.ToShortDateString(), patronType);

            long availableCopyId = DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentId, UserId = patronId }).FirstOrDefault();

            DatabaseHelper.Execute("dbo.spCopies_takeCopyWithReturningDate @CopyId, @UserId, @ReturningDate", new { CopyId = availableCopyId, UserId = patronId, ReturningDate = returningDate });
        }

        public static void ReturnDocument(long documentId, long userId)
        {
            DatabaseHelper.Execute("dbo.spCopies_ReturnDocument @DocumentId, @UserId", new { DocumentId = documentId, UserId = userId });
            Patron[] patrons = UsersDataManager.GetQueueToDocument(documentId);
            if (patrons.Length != 0)
            {
                Takable takable = GetTakableById(documentId);
                NotificationsDataManager.AddNotification(new Notification() {
                    PatronId = patrons[0].CardNumber,
                    Message =  takable.Title + " now waiting for you." });
                DatabaseHelper.Execute("dbo.spQueue_RemovePatronByDocumentId @DocumentId, @PatronId", new { DocumentId = documentId, PatronId = userId });
            }
        }

        public static bool IsAvailable(long documentID, long userID)
        {
            return DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentID, UserId = userID }).ToList().Count != 0;
        }

        public static string GetType(long documentId)
        {
            return DatabaseHelper.Query<string>("dbo.spDocuments_GetType @DocumentId", new { DocumentId = documentId }).FirstOrDefault();
        }

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

        public static void DeleteCopy(long id)
        {
            DatabaseHelper.Execute("dbo.spCopies_DeleteCopy @CopyId", new { CopyId = id });
        }

        public static int GetNumberOfDocuments()
        {
            return DatabaseHelper.Query<int>("dbo.spDocuments_GetNumberOfDocuments", null).FirstOrDefault();
        }

        public static int GetNumberOfCopies()
        {
            return DatabaseHelper.Query<int>("dbo.spCopies_GetNumberOfCopies", null).FirstOrDefault();
        }

        public static void AddCopies(int number, Copy copy)
        {
            DatabaseHelper.Execute("dbo.spCopies_AddCopy @N, @BookId, @Room, @Level", new { N = number, BookId = copy.DocumentId, Room = copy.Room, Level = copy.Level });
        }

        public static Copy[] GetAllCopiesList()
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetAll", null);
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        public static Copy[] GetCheckedCopiesList()
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetChecked", null);
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        public static Copy[] GetCheckedCopiesByPatronId(long userId)
        {
            var output = DatabaseHelper.Query<Copy>("dbo.spCopies_GetCheckedByUser @UserId", new { UserId = userId });
            if (output == null)
                return new Copy[] { };
            return output.ToArray();
        }

        public static AVMaterial[] GetAllAVMaterialsList()
        {
            var output = DatabaseHelper.Query<AVMaterial>("dbo.spAudioVideos_GetAll", null);
            if (output == null)
                return new AVMaterial[] { };
            return output.ToArray();
        }

        public static Book[] GetAllBooksList()
        {
            var output = DatabaseHelper.Query<Book>("dbo.spBooks_GetAll", null);
            if (output == null)
                return new Book[] { };
            return output.ToArray();
        }

        public static InnerMaterial[] GetAllInnerMaterialsList()
        {
            var output = DatabaseHelper.Query<InnerMaterial>("dbo.spInnerMaterials_GetAll", null);
            if (output == null)
                return new InnerMaterial[] { };
            return output.ToArray();
        }

        public static Journal[] GetAllJournalsList()
        {
            var output = DatabaseHelper.Query<Journal>("dbo.spJournals_GetAll", null);
            if (output == null)
                return new Journal[] { };
            return output.ToArray();
        }

        public static Article[] GetAllArticlesList()
        {
            var output = DatabaseHelper.Query<Article>("dbo.spJournalArticles_GetAll", null);
            if (output == null)
                return new Article[] { };
            return output.ToArray();
        }

        public static long GetDocumentId(Document document)
        {
            return DatabaseHelper.Query<Document>("dbo.spDocuments_GetDocumentId @Title, @Authors, @KeyWords",
                new { Title = document.Title, Authors = document.Authors, KeyWords = document.KeyWords }).FirstOrDefault().Id;
        }

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

        public static void RenewCopy(long documentId, long patronId)
        {
            Copy[] copies = GetCheckedCopiesByPatronId(patronId);
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
            string returningDate = takable.EvaluateReturnDate(copy.ReturningDate, patronType);
            DatabaseHelper.Execute("dbo.spCopies_RenewDocument @DocumentId, @PatronId, @ReturningDate", new {
                DocumentId = documentId,
                PatronId = patronId,
                ReturningDate = returningDate});
        }

        public static Takable GetTakableById(long documentId)
        {
            string documentType = GetType(documentId);
            Takable takable = DatabaseHelper.Query<Book>("dbo.spTakable_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray()[0];            
            return takable;
        }

        public static Takable[] GetCheckedByPatronId(long patronId)
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