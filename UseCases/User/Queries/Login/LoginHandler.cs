using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Encription;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Common.User.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthUserDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public LoginHandler(IDbContext dbContext, IJwtTokenProvider jwtTokenProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
        }

        public async Task<AuthUserDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var hashPassword = Sha256Encription.Encript(request.Password);
            var user = await _dbContext.Participants
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashPassword);

            if (user is null)
                throw new UserNotFoundException();

            if (!user.Roles.Any(x => x.Role == request.Role))
                throw new AccessDeniedException();

            var claims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Role))
                .ToList();
            claims.Add(new Claim("id", user.Id.ToString()));

            return new AuthUserDto(_jwtTokenProvider.CreateToken(claims), user.State == Entities.Users.States.ActiveState.FirstSign);
        }
    }
}
