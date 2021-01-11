using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Utility
{
    public interface IDbMigrationService
    {
        Task ApplyMigration();
    }
}
