using LISY.Entities.Users.Patrons;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddFacultyRequest: AddUserRequest
    {
        public Faculty Faculty { get; set; }
    }
}
