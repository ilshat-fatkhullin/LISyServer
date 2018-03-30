namespace LISY.Entities.Requests.Librarian.Put
{
    public class EditUserCredentialsRequest
    {
        public long UserId { get; set; }
        public string Password { get; set; }
    }
}
