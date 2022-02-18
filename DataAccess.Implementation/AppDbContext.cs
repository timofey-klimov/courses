using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Implementation
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
