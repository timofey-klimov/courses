using Authorization.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.User;
using Filter.Implement.Participant;
using Filter.Interfaces;
using Filter.Interfaces.FilterTypes;
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
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetParticipantsRequestHandler(IDbContext dbContext,ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<ParticipantDto>> Handle(GetParticipantsRequest request, CancellationToken cancellationToken)
        {
            var filter = new ParticipantFilter()
            {
                Name = new StringFilter(request.Name),
                Login = new StringFilter(request.Login),
                Surname = new StringFilter(request.Surname)
            };

            var spec = new OnlyActiveParticipantSpecification(request.IsOnlyActive);

            var baseQuery = _dbContext.Participants
                .Include(x => x.Role)
                .Where(x => x.CreatedBy == _currentUserProvider.GetUserId());

            var count = await baseQuery
                .Where(spec.CreateCriteria())
                .Filter(filter, FilterCondition.And)
                .CountAsync();

            var participants = baseQuery
                .Where(spec.CreateCriteria())
                .Filter(filter, FilterCondition.And)
                .Skip(request.Offset)
                .Take(request.Limit)
                .OrderBy(x => x.Id)
                .AsEnumerable();

            return new Pagination<ParticipantDto>(participants.Select(x => new ParticipantDto(x.Id, x.Login, x.Name, x.Surname, x.GetState(), x.Role.Name)), count);
        }
    }
}
