using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Encription;
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
            _dbContext = dbContext;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<AuthUserDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var hashPassword = Sha256Encription.Encript(request.Password);
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashPassword);

            if (user is null)
                throw new UserNotFoundException();

            if (user.Role.ToString() != request.Role.ToString())
                throw new AccessDeniedException();

            return new AuthUserDto(_jwtTokenProvider.CreateToken(new Claim[] { new Claim("id", user.Id.ToString())}), user.Role.ToEnum<Common.User.Model.UserRole>());
        }
    }
}
