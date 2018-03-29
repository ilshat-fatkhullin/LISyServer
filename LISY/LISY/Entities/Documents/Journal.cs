namespace LISY.Entities.Documents
{
    public class Journal
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public int Issue { get; set; }

        public string Authors { get; set; }

        public string PublicationDate { get; set; }

        public int Price { get; set; }

        public string KeyWords { get; set; }

        public string CoverURL { get; set; }
    }
}
