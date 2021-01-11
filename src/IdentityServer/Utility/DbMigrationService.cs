using AuthManagement.IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuthManagement.IdentityServer.Utility
{
    public class DbMigrationService : IDbMigrationService
    {

        public DbMigrationService(ApplicationDbContext identityDbContext
            , PersistedGrantDbContext persistedGrantDbContext
            , ConfigurationDbContext configurationDbContext)
        {
            IdentityDbContext = identityDbContext;
            PersistedGrantDbContext = persistedGrantDbContext;
            ConfigurationDbContext = configurationDbContext;
        }

        public ApplicationDbContext IdentityDbContext { get; }
        public PersistedGrantDbContext PersistedGrantDbContext { get; }
        public ConfigurationDbContext ConfigurationDbContext { get; }

        public async Task ApplyMigration()
        {
            await IdentityDbContext.Database.MigrateAsync();
            await PersistedGrantDbContext.Database.MigrateAsync();
            await ConfigurationDbContext.Database.MigrateAsync();
        }
    }
}
