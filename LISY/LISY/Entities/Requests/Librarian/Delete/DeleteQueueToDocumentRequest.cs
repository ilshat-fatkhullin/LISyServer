namespace LISY.Entities.Requests.Librarian.Delete
{
    public class DeleteQueueToDocumentRequest
    {
        public int LibrarianId { get; set; }
        public long DocumentId { get; set; }
    }
}
