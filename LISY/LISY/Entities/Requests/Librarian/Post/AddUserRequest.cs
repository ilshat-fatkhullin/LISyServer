using LISY.Entities.Users;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddUserRequest
    {        
        public int LibrarianId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
