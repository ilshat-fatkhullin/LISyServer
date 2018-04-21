using LISY.Entities.Documents;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AVMaterialRequest
    {
        public int LibrarianId { get; set; }
        public AVMaterial AVMaterial { get; set; }
    }
}
