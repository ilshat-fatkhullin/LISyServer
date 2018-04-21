using LISY.Entities.Users.Patrons;

namespace LISY.Entities.Requests.Librarian.Put
{
    public class EditStudentRequest
    {
        public int LibrarianId { get; set; }
        public Student Student { get; set; }
    }
}
