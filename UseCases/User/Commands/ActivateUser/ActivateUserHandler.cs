using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.User.Commands.ActivateUser
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserRequest>
    {
        private readonly ICurrentUserProvider _userProvider;
        private readonly IDbContext _dbContext;

        public ActivateUserHandler(ICurrentUserProvider currentUserProvider, IDbContext dbContext)
        {
            _userProvider = currentUserProvider;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.GetUserAsync();

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == currentUser.Id, cancellationToken);

            var password = Shared.Encription.Sha256Encription.Encript(request.Password);
            user.Acvivate(password);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
