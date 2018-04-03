namespace LISY.Entities.Fines
{
    public class Fine
    {
        public long PatronId { get; set; }
        public long DocumentId { get; set; }
        public int FineAmount { get; set; }
    }
}
