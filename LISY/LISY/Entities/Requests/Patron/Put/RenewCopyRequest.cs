namespace LISY.Entities.Requests.Patron.Put
{
    public class RenewCopyRequest
    {
        public long DocumentId { get; set; }
        
        public long PatronId { get; set; }
    }
}
