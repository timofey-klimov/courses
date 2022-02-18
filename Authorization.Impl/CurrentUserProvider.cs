using Authorization.Interfaces;
using Authorization.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Authorization.Impl
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }
        public CurrentUser GetUser()
        {
            var user = _contextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return default;

            var id = Guid.Parse(user.Claims.Where(x => x.Type == "id").FirstOrDefault().Value);
            var name = user.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;
            var surname = user.Claims.Where(x => x.Type == "surname").FirstOrDefault()?.Value;
            var login = user.Claims.Where(x => x.Type == "login").FirstOrDefault()?.Value;

            return new CurrentUser(id, name, login, surname);
        }
    }
}
