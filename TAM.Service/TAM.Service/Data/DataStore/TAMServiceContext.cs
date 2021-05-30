using Microsoft.EntityFrameworkCore;
using TAM.Service.Data.Entities;

namespace TAM.Service.Data.DataStore
{
    public class TAMServiceContext : DbContext
    {
        public TAMServiceContext(DbContextOptions<TAMServiceContext> options) :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<LoginDetails> UserCredentials { get; set; }
        public DbSet<Session> Sessions { get; set; }

    }


    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}