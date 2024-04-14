using Microsoft.AspNetCore.Identity; 

namespace PROJECT.Models
{
    public class Admin : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
