namespace LISY.Entities.Documents
{
    /// <summary>
    /// Contatins information about the article
    /// </summary>
    public class Article: Document
    {
        /// <summary>
        /// Id of journal where current article placed
        /// </summary>
        public long JournalId { get; set; }
    }
}
