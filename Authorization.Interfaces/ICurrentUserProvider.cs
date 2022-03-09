using Authorization.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization.Interfaces
{
    public interface ICurrentUserProvider
    {
        Task<CurrentUser> GetUserAsync();

        bool IsAuth();

        bool IsAdmin();

        bool IsManager();

        bool IsUser();

        int GetUserId();

        IEnumerable<UserRole> GetUserRoles();
    }
}
