using Entities;
using Entities.Users;
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

        DbSet<Test> Tests { get; }

        DbSet<UserRole> Roles { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
