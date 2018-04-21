using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class InnerMaterialRequest
    {
        public int LibrarianId { get; set; }
        public InnerMaterial InnerMaterial { get; set; }
    }
}
