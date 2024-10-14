namespace TicketMaster.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string ArtistName { get; set; }
        public string CoverPictureUrl { get; set; }
    }

}
