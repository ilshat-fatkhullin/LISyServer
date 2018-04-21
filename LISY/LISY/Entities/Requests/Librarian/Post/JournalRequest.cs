using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class JournalRequest
    {
        public int LibrarianId { get; set; }
        public Journal Journal { get; set; }
    }
}
