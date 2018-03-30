using LISY.Entities.Users.Patrons;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddGuestRequest: AddUserRequest
    {
        public Guest Guest { get; set; }
    }
}
