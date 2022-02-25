using Authorization.Interfaces.Models;
using System.Threading.Tasks;

namespace Authorization.Interfaces
{
    public interface ICurrentUserProvider
    {
        Task<CurrentUser> GetUserAsync();
    }
}
