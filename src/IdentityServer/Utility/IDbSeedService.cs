using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Utility
{
    public interface IDbSeedService
    {
        Task SeedIdentityDb();
        Task SeedIdentityServerDb();
    }
}
