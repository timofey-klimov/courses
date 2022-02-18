using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Implementation
{
    public class MigrationDbContext : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optBuilder.UseSqlServer("Server=DESKTOP-JDVB3O9\\SQLEXPRESS01;Database=FlatBot;Trusted_Connection=True;");

            return new AppDbContext(optBuilder.Options);
        }
    }
}
