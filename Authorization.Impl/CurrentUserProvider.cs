using Authorization.Interfaces;
using Authorization.Interfaces.Models;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Linq;
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

            var id = Guid.Parse(contextUser.Claims.Where(x => x.Type == "id").FirstOrDefault().Value);

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            var role = user.Role.ToEnum<UserRole>();

            return new CurrentUser(user.Id, user.Name, user.Login, user.Surname, role);
        }

        /// <summary>
        /// Проверяет является ли админом текущий пользователь
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsAdmin()
        {
            return (await this.GetUserAsync())?.Role == UserRole.Admin;
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
    }
}
