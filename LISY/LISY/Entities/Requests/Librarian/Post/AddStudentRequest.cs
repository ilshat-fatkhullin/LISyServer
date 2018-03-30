using LISY.Entities.Users.Patrons;

namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddStudentRequest: AddUserRequest
    {
        public Student Student { get; set; }
    }
}
