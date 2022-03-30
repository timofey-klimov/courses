using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace DataAccess.Implementation
{
    public class MigrationDbContext : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optBuilder.UseSqlServer("Server=DESKTOP-1O8U0H5\\SQLEXPRESS;Database=Courses;Trusted_Connection=True;");

            return new AppDbContext(optBuilder.Options, default, new Logger<AppDbContext>(new LoggerFactory()), default);
        }
    }
}
