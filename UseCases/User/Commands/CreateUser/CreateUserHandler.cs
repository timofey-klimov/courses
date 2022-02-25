using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Encription;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, AuthUserDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IJwtTokenProvider _tokenProvider;
        public CreateUserHandler(IDbContext dbContext, IJwtTokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<AuthUserDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = Sha256Encription.Encript(request.Password);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashedPassword, cancellationToken);

            if (user is not null)
                throw new UserExistedException();

            if (_dbContext.Users.Any(x => x.Login == request.Login))
                throw new LoginIsUsedException();

            var role = (UserRole)Enum.Parse(typeof(UserRole), request.UserRole.ToString());

            var createdUser = new Entities.User(request.Name, request.Surname, request.Login, hashedPassword, role);

            await _dbContext.Users.AddAsync(createdUser);
            await _dbContext.SaveChangesAsync();

            return new AuthUserDto(_tokenProvider.CreateToken(new Claim[] 
                                    { new Claim("id", createdUser.Id.ToString()),
                                      new Claim(ClaimTypes.Role, createdUser.Role.ToString())
                                    }));
        }
    }
}
