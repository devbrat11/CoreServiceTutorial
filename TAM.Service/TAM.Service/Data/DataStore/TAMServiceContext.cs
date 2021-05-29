using TAMService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TAM.Service.Data.Entities;
using System;

namespace TAMService.Data.DataStore
{
    public class TAMServiceContext : DbContext
    {
        public TAMServiceContext(DbContextOptions<TAMServiceContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<User>().Property(x => x.PK).HasDefaultValueSql("NEWID()");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
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