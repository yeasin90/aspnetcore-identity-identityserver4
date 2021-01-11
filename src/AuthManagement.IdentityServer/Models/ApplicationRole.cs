using AuthManagement.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthManagement.IdentityServer.Data
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
