using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;

namespace UseCases.Participant.Commands.BlockParticipantCommand
{
    public class BlockParticipantRequestHandler : IRequestHandler<BlockParticipantRequest, int>
    {
        private readonly IDbContext _dbContext;
        public BlockParticipantRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> Handle(BlockParticipantRequest request, CancellationToken cancellationToken)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (participant is null)
                throw new ParticipantNotFoundException();

            participant.Block();

            await _dbContext.SaveChangesAsync();

            return participant.Id;
        }
    }
}
