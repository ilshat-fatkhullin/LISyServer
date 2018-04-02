namespace LISY.Entities.Documents
{
    public class Copy
    {
        public long Id { get; set; }

        public long DocumentId { get; set; }

        public long PatronId { get; set; }

        public bool Checked { get; set; }

        public int Room { get; set; }

        public int Level { get; set; }

        public string ReturningDate { get; set; }

        public bool IsRenewed { get; set; }
    }
}
