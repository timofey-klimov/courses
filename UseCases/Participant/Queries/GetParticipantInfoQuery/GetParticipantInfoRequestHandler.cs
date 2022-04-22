using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Queries.GetParticipantInfoQuery
{
    public class GetParticipantInfoRequestHandler : IRequestHandler<GetParticipantInfoRequest, ParticipantDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetParticipantInfoRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }
        public async Task<ParticipantDto> Handle(GetParticipantInfoRequest request, CancellationToken cancellationToken)
        {
            var participant = await _dbContext.Participants
                .Include(x => x.Avatar)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (participant.IsBlocked())
                throw new ParticipantBlockedException();

            return new ParticipantDto(
                participant.Id, participant.Login, participant.Name, participant.Surname, participant.GetState(), participant.Role.Name);
        }
    }
}
