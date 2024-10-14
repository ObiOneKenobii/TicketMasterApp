using Microsoft.AspNetCore.Identity;

namespace TicketMaster.Models
{
    public class User: IdentityUser 
    {
        public string? Name { get; set; }
    }
}
