using Dapper;
using LISY.Entities.Documents;
using LISY.Helpers;
using System;
using System.Data;
using System.Linq;

namespace LISY.DataManagers
{
    /// <summary>
    /// Contains database commands for documents
    /// </summary>
    public static class DocumentsDataManager
    {
        public static void AddAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spAudioVideos_AddAV @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);
            }
        }

        public static void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spBooks_AddBook @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
                        book);
            }
        }

        public static void AddInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spInnerMaterials_AddInnerMaterial @Title, @Authors, @Type, @Room, @Level, @KeyWords, @CoverURL",
                        innerMaterial);
            }
        }

        public static void AddJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spJournals_AddJournal @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
                        journal);
            }
        }

        public static void AddArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spJournalArticles_AddJournalArticle @Title, @Authors, @JournalId, @KeyWords, @CoverURL",
                        article);
            }
        }
        
        public static void DeleteDocument(int id)
        {                        
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spDocuments_DeleteDocument @Id", new { Id = id });
            }
        }

        public static void EditAVMaterial(AVMaterial avMaterial)
        {
            if (avMaterial == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spAudioVideos_ModifyAV @Id, @Title, @Authors, @KeyWords, @CoverURL, @Price",
                        avMaterial);
            }
        }

        public static void EditBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spBooks_ModifyBook @Id, @Title, @Authors, @Publisher, @Edition, @Year, @IsBestseller, @KeyWords, @CoverURL, @Price",
                        book);
            }
        }

        public static void EditInnerMaterial(InnerMaterial innerMaterial)
        {
            if (innerMaterial == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spInnerMaterials_ModifyInnerMaterial @Id, @Title, @Authors, @KeyWords",
                        innerMaterial);
            }
        }

        public static void EditJournal(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spJournals_ModifyJournal @Id, @Title, @Authors, @Publisher, @Issue, @PublicationDate, @KeyWords, @CoverURL, @Price",
                        journal);
            }
        }

        public static void EditArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {                
                connection.Execute("dbo.spJournalArticles_ModifyJournalArticle @Id, @Title, @Authors, @KeyWords",
                        article);
            }
        }
        
        public static void CheckOutDocument(long documentId, long userId)
        {
            if (!IsAvailable(documentId, userId))
                return;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                string documentType = GetType(documentId);
                string patronType = connection.Query<string>("dbo.spUsers_GetType @UserId", new { UserId = userId }).FirstOrDefault();
                string date = DocumentsHelper.EvaluateReturnDate(patronType);

                if (documentType == "Inner")
                {
                    return;
                }
                else if (documentType == "Book")
                {
                    Book[] documents = connection.Query<Book>("dbo.spBooks_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();                    
                }
                else if (documentType == "AV")
                {                    
                    AVMaterial[] documents = connection.Query<AVMaterial>("dbo.spAudioVideos_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();                    
                }
                else if (documentType == "Journal Article")
                {                    
                    Journal[] documents = connection.Query<Journal>("dbo.spJournals_GetAllById @DocumentId", new { DocumentId = documentId }).ToArray();                    
                }

                long availableCopyId = connection.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentId, UserId = userId }).FirstOrDefault();
                connection.Execute("dbo.spCopies_takeCopyWithReturningDate @CopyId, @UserId, @ReturningDate", new { CopyId = availableCopyId, UserId = userId, ReturningDate = date });
            }
        }

        public static void ReturnDocument(long documentId, long userId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                var output = connection.Execute("dbo.spCopies_ReturnDocument @DocumentId, @UserId", new { DocumentId = documentId, UserId = userId });
            }
        }

        public static bool IsAvailable(long documentID, long userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                var output = connection.Query<long>("dbo.spCopies_GetAvailableCopies @BookId, @UserId", new { BookId = documentID, UserId = userID }).ToList();
                return (output.Count != 0);
            }
        }
        
        public static string GetType(long documentId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                var output = connection.Query<string>("dbo.spDocuments_GetType @DocumentId", new { DocumentId = documentId }).ToList();
                return (output[0]);
            }
        }      

        public static int GetCopyId(Copy copy)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                var output = connection.Query<int>("dbo.spCopies_GetCopyId @DocId, @Room, @Level", new { DocId = copy.BookId, copy.Room, copy.Level }).ToList();
                if (output.Count() > 0)
                {
                    return output[0];
                }
                else
                {
                    return -1;
                }
            }
        }

        public static void DeleteCopyByDocumentId(Copy copy)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spCopies_DeleteCopyByDocumentIdRoomLevel @DocId, @Room, @Level", new { DocId = copy.BookId, copy.Room, copy.Level });
            }
        }

        public static void DeleteCopy(Copy copy)
        {
            if (copy == null)
                throw new ArgumentNullException();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                connection.Execute("dbo.spCopies_DeleteCopy @CopyId", new { CopyId = copy.CopyId });
            }
        }

        public static int GetNumberOfDocuments()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<int>("dbo.spDocuments_GetNumberOfDocuments").FirstOrDefault();
            }
        }

        public static int GetNumberOfCopies()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<int>("dbo.spCopies_GetNumberOfCopies").FirstOrDefault();                
            }
        }

        public static void AddCopy(int number, Copy copy)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                var output = connection.Execute("dbo.spCopies_AddCopy @N, @BookId, @Room, @Level", new { N = number, BookId = copy.BookId, Room = copy.Room, Level = copy.Level });
            }
        }

        public static Copy[] GetAllCopiesList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Copy>("dbo.spCopies_GetAll").ToArray();
            }
        }

        public static Copy[] GetCheckedCopiesList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Copy>("dbo.spCopies_GetChecked").ToArray();
            }
        }

        public static Copy[] GetCheckedByUserCopiesList(long userId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Copy>("dbo.spCopies_GetCheckedByUser @UserId", new { UserId = userId }).ToArray();
            }
        }

        public static AVMaterial[] GetAllAVMaterialsList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<AVMaterial>("dbo.spAudioVideos_GetAll").ToArray();                
            }
        }

        public static Book[] GetAllBooksList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Book>("dbo.spBooks_GetAll").ToArray();
            }
        }

        public static InnerMaterial[] GetAllInnerMaterialsList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<InnerMaterial>("dbo.spInnerMaterials_GetAll").ToArray();                
            }
        }

        public static Journal[] GetAllJournalsList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Journal>("dbo.spJournals_GetAll").ToArray();                
            }
        }

        public static Article[] GetAllJournalArticlesList()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                return connection.Query<Article>("dbo.spJournalArticles_GetAll").ToArray();                
            }
        }
    }
}
