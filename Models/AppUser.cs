using Microsoft.AspNetCore.Identity;

namespace to_do_app.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
