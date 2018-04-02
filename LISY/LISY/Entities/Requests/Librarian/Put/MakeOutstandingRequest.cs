namespace LISY.Entities.Requests.Librarian.Put
{
    public class MakeOutstandingRequest
    {
        public bool State { get; set; }

        public long DocumentId { get; set; }
    }
}
