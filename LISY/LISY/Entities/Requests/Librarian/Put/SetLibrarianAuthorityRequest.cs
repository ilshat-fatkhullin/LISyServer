namespace LISY.Entities.Requests.Librarian.Put
{
    public class SetLibrarianAuthorityRequest
    {
        public long LibrarianId { get; set; }
        public int Authority { get; set; }
    }
}
