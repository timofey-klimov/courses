using Authorization.Interfaces;
using Authorization.Interfaces.Models;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.Impl
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDbContext _dbContext;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor, IDbContext dbContext)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Получает текущего пользователя в рамках запроса
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentUser> GetUserAsync()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            var id = int.Parse(contextUser.Claims.Where(x => x.Type == "id").FirstOrDefault().Value);

            var user = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == id);

            var roles = user.Roles.Select(x => new UserRole(x.Role));

            return new CurrentUser(user.Name, user.Login, user.Surname, roles);
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

        public IEnumerable<UserRole> GetUserRoles()
        {
            var contextUser = _contextAccessor.HttpContext?.User;

            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
                return default;

            return contextUser.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => new UserRole(x.Value));
        }
    }
}
