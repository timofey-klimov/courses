using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Base;
using Entities.Events;
using Entities.Participants;
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
        private readonly ICurrentUserProvider _userProvider;

        public AppDbContext(DbContextOptions<AppDbContext> opts, IMediator mediator, ILogger<AppDbContext> logger, ICurrentUserProvider userProvider)
            : base(opts)
        {
            _mediatr = mediator;
            _logger = logger;
            _userProvider = userProvider;
        }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<ParticipantRole> Roles { get; set; }

        public DbSet<StudyGroup> StudyGroups { get; set; }

        public DbSet<Avatar> Avatars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = new List<DomainEvent>();

            var entites = this.ChangeTracker.Entries()
                .Select(x => x.Entity)
                .OfType<IDomainEventProvider>();


            foreach (var entity in entites)
            {
                events.AddRange(entity?.Events);
                entity.Events?.Clear();
            }

            var auditableEntites = this.ChangeTracker
                .Entries<IAutitableEntity>();

            foreach (var entityEntry in auditableEntites)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.Create(_userProvider.GetUserId());
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Entity.Update(_userProvider.GetUserId());
                }
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
