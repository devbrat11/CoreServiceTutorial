using CoreService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreService.Data
{
    public class CoreServiceContext : DbContext
    {
        public CoreServiceContext(DbContextOptions<CoreServiceContext> options) :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }

    }
}