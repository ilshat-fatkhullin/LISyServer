namespace LISY.Entities.Notifications
{
    public class Notification
    {
        public long Id { get; set; }
        public long PatronId { get; set; }
        public string Message { get; set; }
        public bool Checked { get; set; }
    }
}
