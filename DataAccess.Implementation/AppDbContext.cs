using DataAccess.Interfaces;
using Entities;
using Entities.Base;
using Entities.Events;
using Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class AppDbContext : DbContext, IDbContext
    {
        private readonly IMediator _mediatr;
        private readonly ILogger<AppDbContext> _logger;

        public AppDbContext(DbContextOptions<AppDbContext> opts, IMediator mediator, ILogger<AppDbContext> logger)
            : base(opts)
        {
            _mediatr = mediator;
            _logger = logger;
        }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = new List<DomainEvent>();

            var entites = this.ChangeTracker.Entries()
                .Select(x => x.Entity)
                .OfType<DomainEventsEntity>();

            foreach (var entity in entites)
            {
                events.AddRange(entity?.Events);
                entity.Events?.Clear();
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            events.ForEach(async x => await _mediatr.Publish(x));

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(x => _logger.LogInformation(x));
        }
    }
}
