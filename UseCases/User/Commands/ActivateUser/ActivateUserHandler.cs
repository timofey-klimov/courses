using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Exceptions;

namespace UseCases.User.Commands.ActivateUser
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserRequest>
    {
        private readonly ICurrentUserProvider _userProvider;
        private readonly IDbContext _dbContext;

        public ActivateUserHandler(ICurrentUserProvider currentUserProvider, IDbContext dbContext)
        {
            _userProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Unit> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
        {
            var userId = _userProvider.GetUserId();

            var user = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                throw new UserNotFoundException();

            var password = Shared.Encription.Sha256Encription.Encript(request.Password);
            user.Activate(password);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
