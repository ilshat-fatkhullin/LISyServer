namespace LISY.Entities.Requests.Librarian.Put
{
    public class MakeOutstandingRequest
    {
        public int LibrarianId { get; set; }

        public bool State { get; set; }

        public long DocumentId { get; set; }
    }
}
