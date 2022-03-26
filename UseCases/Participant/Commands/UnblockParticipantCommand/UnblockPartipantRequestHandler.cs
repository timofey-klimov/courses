using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;

namespace UseCases.Participant.Commands.UnblockParticipantCommand
{
    public class UnblockPartipantRequestHandler : IRequestHandler<UnblockParticipantRequest, int>
    {
        private readonly IDbContext _dbContext;
        public UnblockPartipantRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> Handle(UnblockParticipantRequest request, CancellationToken cancellationToken)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (participant is null)
                throw new ParticipantNotFoundException();

            participant.Unblock();

            await _dbContext.SaveChangesAsync();

            return participant.Id;
        }
    }
}
