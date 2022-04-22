using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;

namespace UseCases.Participant.Queries.GetParticipantAvatarQuery
{
    public class GetParticipantAvatarRequestHandler : IRequestHandler<GetParticipantAvatarRequest, byte[]>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetParticipantAvatarRequestHandler(
            IDbContext dbContext,
            ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<byte[]> Handle(GetParticipantAvatarRequest request, CancellationToken cancellationToken)
        {
            var participant = await _dbContext.Participants
                .Include(x => x.Avatar)
                .Select(x => new
                {
                    Id = x.Id,
                    Avatar = x.Avatar.Content
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (participant == null)
                throw new ParticipantNotFoundException();

            return participant.Avatar;
        }
    }
}
