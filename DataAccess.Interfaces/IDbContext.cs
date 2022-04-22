using Entities;
using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<Participant> Participants { get; }

        DbSet<ParticipantRole> Roles { get; }

        DbSet<StudyGroup> StudyGroups { get; }

        DbSet<Avatar> Avatars { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
