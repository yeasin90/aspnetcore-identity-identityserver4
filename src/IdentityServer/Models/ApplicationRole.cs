using Microsoft.AspNetCore.Identity;

namespace AuthManagement.IdentityServer.Models
{
    public class ApplicationRole : IdentityRole
    {
        public RoleTypes RoleType { get; set; }
        public ApplicationRole(RoleTypes roleType) : base(roleType.ToString())
        {
            RoleType = roleType;
        }
    }
}
