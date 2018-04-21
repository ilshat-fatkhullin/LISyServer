using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class ArticleRequest
    {
        public int LibrarianId { get; set; }
        public Article Article { get; set; }
    }
}
