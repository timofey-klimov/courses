using DataAccess.Interfaces;
using Entities;
using Entities.Base;
using Entities.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public AppDbContext(DbContextOptions<AppDbContext> opts, IMediator mediator)
            : base(opts)
        {
            _mediatr = mediator;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = new List<DomainEvent>();

            var entites = this.ChangeTracker.Entries()
                .Select(x => x.Entity)
                .Cast<DomainEventsEntity>();

            foreach (var entity in entites)
            {
                events.AddRange(entity?.Events);
                entity.Events?.Clear();
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            events.ForEach(async x => await _mediatr.Publish(x));

            return result;
        }
    }
}
