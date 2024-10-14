namespace TicketMaster.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Beverage> Beverages { get; set; } = new List<Beverage>(); // Assuming a cart holds multiple beverages
        public decimal TotalPrice { get; set; } // Optionally track the total price of the cart
    }
}
