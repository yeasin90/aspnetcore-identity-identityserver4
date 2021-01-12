using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthManagement.IdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
        public Claim[] Claims { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
