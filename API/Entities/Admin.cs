using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class Admin : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailConfirmationCode { get; set; }
        public DateTime? EmailConfirmationExpiry { get; set; }
    }
}