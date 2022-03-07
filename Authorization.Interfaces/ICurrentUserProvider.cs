using Authorization.Interfaces.Models;
using System;
using System.Threading.Tasks;

namespace Authorization.Interfaces
{
    public interface ICurrentUserProvider
    {
        Task<CurrentUser> GetUserAsync();

        bool IsAuth();

        bool IsAdmin();

        Guid GetUserId();

        UserRole GetUserRole();
    }
}
