using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class BookRequest
    {
        public int LibrarianId { get; set; }
        public Book Book { get; set; }
    }
}
