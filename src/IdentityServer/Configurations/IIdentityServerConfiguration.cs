using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Configurations
{
    public interface IIdentityServerConfiguration
    {
        Task<IEnumerable<ApiScope>> GetApiScopes();
        Task<IEnumerable<ApiResource>> GetApiResources();
        Task<List<Client>> GetIdentityServerClients();
    }
}
