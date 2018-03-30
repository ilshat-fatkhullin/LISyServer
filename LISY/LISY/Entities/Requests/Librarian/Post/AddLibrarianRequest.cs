namespace LISY.Entities.Requests.Librarian.Post
{
    public class AddLibrarianRequest: AddUserRequest
    {
        public Users.Librarian Librarian { get; set; }
    }
}
