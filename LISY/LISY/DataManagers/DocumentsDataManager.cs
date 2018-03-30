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
        public static void AddAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spAudioVideos_AddAV @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);
        }

        public static void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spBooks_AddBook @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
                        book);
        }

        public static void AddInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spInnerMaterials_AddInnerMaterial @Title, @Authors, @Type, @Room, @Level, @KeyWords, @CoverURL",
                        innerMaterial);
        }

        public static void AddJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournals_AddJournal @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
                        journal);
        }

        public static void AddArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            DatabaseHelper.Execute("dbo.spJournalArticles_AddJournalArticle @Title, @Authors, @JournalId, @KeyWords, @CoverURL",
                        article);
        }

        public static void DeleteDocument(long id)
        {
            DatabaseHelper.Execute("dbo.spDocuments_DeleteDocument @Id", new { Id = id });
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

        public static void CheckOutDocument(long documentId, long userId)
        {
            if (!IsAvailable(documentId, userId))
                return;

            string documentType = GetType(documentId);
            string patronType = DatabaseHelper.Query<string>("dbo.spUsers_GetType @UserId", new { UserId = userId }).FirstOrDefault();
            string date = DocumentsHelper.EvaluateReturnDate(patronType);

            if (documentType == "Inner")
            {
                return;
            }
            else if (documentType == "Book")
            {
                Book[] documents = DatabaseHelper.Query<Book>("dbo.spBooks_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();
            }
            else if (documentType == "AV")
            {
                AVMaterial[] documents = DatabaseHelper.Query<AVMaterial>("dbo.spAudioVideos_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();
            }
            else if (documentType == "Journal Article")
            {
                Journal[] documents = DatabaseHelper.Query<Journal>("dbo.spJournals_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();
            }

            long availableCopyId = DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentId, UserId = userId }).FirstOrDefault();
            DatabaseHelper.Execute("dbo.spCopies_takeCopyWithReturningDate @CopyId, @UserId, @ReturningDate", new { CopyId = availableCopyId, UserId = userId, ReturningDate = date });
        }

        public static void ReturnDocument(long documentId, long userId)
        {
            DatabaseHelper.Execute("dbo.spCopies_ReturnDocument @DocumentId, @UserId", new { DocumentId = documentId, UserId = userId });
        }

        public static bool IsAvailable(long documentID, long userID)
        {
            var output = DatabaseHelper.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentID, UserId = userID }).ToList();
            return (output.Count != 0);
        }

        public static string GetType(long documentId)
        {
            return DatabaseHelper.Query<string>("dbo.spDocuments_GetType @DocumentId", new { DocumentId = documentId }).FirstOrDefault();
        }

        public static int GetCopyId(Copy copy)
        {
            var output = DatabaseHelper.Query<int>("dbo.spCopies_GetCopyId @DocId, @Room, @Level", new { DocId = copy.BookId, copy.Room, copy.Level }).ToList();
            if (output.Count() > 0)
            {
                return output[0];
            }
            else
            {
                return -1;
            }
        }

        public static void DeleteCopyByDocumentId(Copy copy)
        {
            DatabaseHelper.Execute("dbo.spCopies_DeleteCopyByDocumentIdRoomLevel @DocId, @Room, @Level", new { DocId = copy.BookId, copy.Room, copy.Level });
        }

        public static void DeleteCopy(Copy copy)
        {
            if (copy == null)
                throw new ArgumentNullException();
            DatabaseHelper.Execute("dbo.spCopies_DeleteCopy @CopyId", new { CopyId = copy.CopyId });
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
            DatabaseHelper.Execute("dbo.spCopies_AddCopy @N, @BookId, @Room, @Level", new { N = number, BookId = copy.BookId, Room = copy.Room, Level = copy.Level });
        }

        public static Copy[] GetAllCopiesList()
        {
            return DatabaseHelper.Query<Copy>("dbo.spCopies_GetAll", null).ToArray();
        }

        public static Copy[] GetCheckedCopiesList()
        {
            return DatabaseHelper.Query<Copy>("dbo.spCopies_GetChecked", null).ToArray();
        }

        public static Copy[] GetCheckedByUserCopiesList(long userId)
        {
            return DatabaseHelper.Query<Copy>("dbo.spCopies_GetCheckedByUser @UserId", new { UserId = userId }).ToArray();
        }

        public static AVMaterial[] GetAllAVMaterialsList()
        {
            return DatabaseHelper.Query<AVMaterial>("dbo.spAudioVideos_GetAll", null).ToArray();
        }

        public static Book[] GetAllBooksList()
        {
            return DatabaseHelper.Query<Book>("dbo.spBooks_GetAll", null).ToArray();
        }

        public static InnerMaterial[] GetAllInnerMaterialsList()
        {
            return DatabaseHelper.Query<InnerMaterial>("dbo.spInnerMaterials_GetAll", null).ToArray();
        }

        public static Journal[] GetAllJournalsList()
        {
            return DatabaseHelper.Query<Journal>("dbo.spJournals_GetAll", null).ToArray();
        }

        public static Article[] GetAllArticlesList()
        {            
            return DatabaseHelper.Query<Article>("dbo.spJournalArticles_GetAll", null).ToArray();            
        }

        public static long GetDocumentId(Document document)
        {
            return DatabaseHelper.Query<Document>("dbo.spDocuments_GetDocumentId @Title, @Authors, @KeyWords",
                new { Title = document.Title, Authors = document.Authors, KeyWords = document.KeyWords }).FirstOrDefault().Id;
        }
    }
}
