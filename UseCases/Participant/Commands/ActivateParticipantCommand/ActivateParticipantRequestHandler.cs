using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Encription;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;

namespace UseCases.Participant.Commands.ActivateParticipantCommand
{
    public class ActivateParticipantRequestHandler : IRequestHandler<ActivateParticipantRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        public ActivateParticipantRequestHandler(IDbContext dbContext, ICurrentUserProvider userProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Unit> Handle(ActivateParticipantRequest request, CancellationToken cancellationToken)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (participant is null)
                throw new ParticipantNotFoundException();

            var password = Sha256Encription.Encript(request.Password);

            participant.Activate(password);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
