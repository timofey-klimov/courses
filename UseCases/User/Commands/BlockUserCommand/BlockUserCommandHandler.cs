using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Dto;

namespace UseCases.User.Commands.BlockUserCommand
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserRequest, int>
    {
        public readonly IDbContext _dbContext;
        public BlockUserCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(BlockUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            user.Block();

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
