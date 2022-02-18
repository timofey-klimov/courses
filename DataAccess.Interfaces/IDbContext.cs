using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<User> Users { get; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
