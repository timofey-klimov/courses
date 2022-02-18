using System.Collections.Generic;
using System.Security.Claims;

namespace Authorization.Interfaces
{
    public interface IJwtTokenProvider
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}
