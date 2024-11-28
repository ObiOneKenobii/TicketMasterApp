namespace TicketMaster.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string ArtistName { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public string CoverPictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}
