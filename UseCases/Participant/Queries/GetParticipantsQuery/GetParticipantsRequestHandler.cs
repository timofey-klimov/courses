using Authorization.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Queries.GetParticipantsQuery
{
    public class GetParticipantsRequestHandler : IRequestHandler<GetParticipantsRequest, Pagination<ParticipantDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IFilterProvider _filterProvider;
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetParticipantsRequestHandler(IDbContext dbContext, IFilterProvider filterProvider, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _filterProvider = filterProvider ?? throw new ArgumentNullException(nameof(filterProvider));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }
        public async Task<Pagination<ParticipantDto>> Handle(GetParticipantsRequest request, CancellationToken cancellationToken)
        {
            var spec = new ParticipantFilterSpecification(request.Name, request.Surname, request.Login, request.IsOnlyActive);

            var baseQuery = _dbContext.Participants
                .Include(x => x.Role)
                .Where(x => x.CreatedBy == _currentUserProvider.GetUserId());

            var count = await _filterProvider.GetCountQuery(baseQuery, spec);

            var participants = _filterProvider.GetQuery(baseQuery, spec)
                .Skip(request.Offset)
                .Take(request.Limit)
                .OrderBy(x => x.Id)
                .AsEnumerable();

            return new Pagination<ParticipantDto>(participants.Select(x => new ParticipantDto(x.Id, x.Login, x.Name, x.Surname, x.GetState(), x.Role.Name)), count);
        }
    }
}
