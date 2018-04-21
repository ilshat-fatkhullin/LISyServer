using LISY.Entities.Users.Patrons;

namespace LISY.Entities.Requests.Librarian.Put
{
    public class EditGuestRequest
    {
        public int LibrarianId { get; set; }
        public Guest Guest { get; set; }
    }
}
