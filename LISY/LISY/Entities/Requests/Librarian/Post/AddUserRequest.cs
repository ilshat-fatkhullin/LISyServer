using LISY.Entities.Users;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddUserRequest
    {        
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
