using Dapper;
using LISY.Entities.Documents;
using LISY.Helpers;
using System;
using System.Data;
using System.Linq;

namespace LISY.DataManagers
{
    public static class DocumentsDataManager
    {
        public static long AddAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_AddAV @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);

            return GetDocumentId(avMaterial);
        }

        public static long AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_AddBook @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
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

            DatabaseHelper.Execute("dbo.spJournals_AddJournal @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
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

        public static void CheckOutDocument(long documentId, long userId)
        {
            if (!IsAvailable(documentId, userId))
                return;

            string documentType = GetType(documentId);
            string patronType = DatabaseHelper.Query<string>("dbo.spUsers_GetType @UserId", new { UserId = userId }).FirstOrDefault();            

            Takable takable = null;

            if (documentType == "Inner")
            {
                return;
            }
            else if (documentType == "Book")
            {
                takable = DatabaseHelper.Query<Book>("dbo.spBooks_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray()[0];
            }
            else if (documentType == "AV")
            {
                takable = DatabaseHelper.Query<AVMaterial>("dbo.spAudioVideos_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray()[0];
            }
            else if (documentType == "Journal Article")
            {
                takable = DatabaseHelper.Query<Journal>("dbo.spJournals_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray()[0];
            }

            string returningDate = takable.EvaluateReturnDate(patronType);

            long availableCopyId = DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentId, UserId = userId }).FirstOrDefault();

            DatabaseHelper.Execute("dbo.spCopies_takeCopyWithReturningDate @CopyId, @UserId, @ReturningDate", new { CopyId = availableCopyId, UserId = userId, ReturningDate = returningDate });
        }

        public static void ReturnDocument(long documentId, long userId)
        {
            DatabaseHelper.Execute("dbo.spCopies_ReturnDocument @DocumentId, @UserId", new { DocumentId = documentId, UserId = userId });
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

        public static Copy[] GetCheckedByUserCopiesList(long userId)
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
    }
}