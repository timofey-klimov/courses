using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Exceptions;

namespace UseCases.User.Commands.UnblockUserCommand
{
    public class UnblockUserRequestHandler : IRequestHandler<UnblockUserRequest, int>
    {
        private readonly IDbContext _dbContext;

        public UnblockUserRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> Handle(UnblockUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
                throw new UserNotFoundException();

            user.Unblock();

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
