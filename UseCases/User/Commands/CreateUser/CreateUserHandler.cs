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
using UseCases.Common.Exceptions;
using UseCases.Common.User.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CreateUserHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }
        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (!_currentUserProvider.IsAdmin())
                throw new AccessDeniedException();

            var hashedPassword = Sha256Encription.Encript(request.Password);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashedPassword, cancellationToken);

            if (user is not null)
                throw new UserExistedException();

            if (await _dbContext.Users.AnyAsync(x => x.Login == request.Login))
                throw new LoginIsNotAvailableException();

            var role = request.UserRole.ToEnum<UserRole>();

            var createdUser = new Entities.User(request.Name, request.Surname, request.Login, request.Password, hashedPassword, role);

            _dbContext.Users.Add(createdUser);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
