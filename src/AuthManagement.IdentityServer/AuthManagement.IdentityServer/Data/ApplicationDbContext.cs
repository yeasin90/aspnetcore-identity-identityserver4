using Microsoft.EntityFrameworkCore;

namespace AuthManagement.IdentityServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
    }
}
