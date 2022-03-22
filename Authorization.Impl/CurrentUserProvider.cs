using Authorization.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Authorization.Impl
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        /// <summary>
        /// Проверяет является ли админом текущий пользователь
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            var roles = contextUser.Claims.Where(x => x.Type == ClaimTypes.Role);

            return roles.Any(x => x.Value == "Admin");
        }


        /// <summary>
        /// Проверяет прошел ли пользователь аутентификацию 
        /// </summary>
        /// <returns></returns>
        public bool IsAuth()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            return contextUser != null && contextUser.Identity?.IsAuthenticated == true;
        }


        public bool IsManager()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            var roles = contextUser.Claims.Where(x => x.Type == ClaimTypes.Role);

            return roles.Any(x => x.Value == "Manager");
        }

        public bool IsUser()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            var roles = contextUser.Claims.Where(x => x.Type == ClaimTypes.Role);

            return roles.Any(x => x.Value == "User");
        }

        public int GetUserId()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            return int.Parse(contextUser.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value);
        }
    }
}
