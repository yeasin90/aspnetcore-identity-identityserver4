using AuthManagement.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Configurations
{
    public interface IIdentityConfiguration
    {
        Task<List<ApplicationRole>> GetApplicationRoles();
        Task<List<ApplicationUser>> GetApplicationUsers();
    }
}
