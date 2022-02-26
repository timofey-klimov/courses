using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
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
    public class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        private readonly IDbContext _dbContext;
        public CreateUserHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = Sha256Encription.Encript(request.Password);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashedPassword, cancellationToken);

            if (user is not null)
                throw new UserExistedException();

            if (_dbContext.Users.Any(x => x.Login == request.Login))
                throw new LoginIsUsedException();

            var role = request.UserRole.ToEnum<UserRole>();

            var createdUser = new Entities.User(request.Name, request.Surname, request.Login, request.Password, hashedPassword, role);

            await _dbContext.Users.AddAsync(createdUser);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
