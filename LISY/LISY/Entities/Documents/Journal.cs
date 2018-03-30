namespace LISY.Entities.Documents
{
    public class Journal: Document
    {
        public string Publisher { get; set; }

        public int Issue { get; set; }        

        public string PublicationDate { get; set; }

        public int Price { get; set; }        
    }
}
