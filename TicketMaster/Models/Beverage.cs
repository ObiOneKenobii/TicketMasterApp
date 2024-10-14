namespace TicketMaster.Models
{
    public class Beverage
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string? CoverImageUrl { get; set; } // For event cover image
    }
}
