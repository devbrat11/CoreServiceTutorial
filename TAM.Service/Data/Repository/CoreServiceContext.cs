using TAMService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TAMService.Data.Repository
{
    public class CoreServiceContext : DbContext
    {
        public CoreServiceContext(DbContextOptions<CoreServiceContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}