using Authorization.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.StudyGroup;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Common.Participant;
using UseCases.StudyGroup.Dto;

namespace UseCases.Teachers.Queries.GetTeachersGroupsQuery
{
    public class GetTeachersGroupsQueryHandler : IRequestHandler<GetTeachersGroupsQueryRequest, Pagination<SimpleStudyGroupDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetTeachersGroupsQueryHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<SimpleStudyGroupDto>> Handle(GetTeachersGroupsQueryRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var spec = new StudyGroupFilterSpecification(request.Title, request.startDate, request.endDate);

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                .Select(x => new
                {
                    Id = x.Id,
                    Groups = x.StudyGroups
                        .AsQueryable()
                        .Where(spec.CreateCriteria())
                        .Skip(request.offset)
                        .Take(request.limit)
                        .AsEnumerable(),
                    Count = x.StudyGroups
                        .Count()
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (result == null)
                throw new ParticipantNotFoundException();
    
           return new Pagination<SimpleStudyGroupDto>(
               result.Groups.Select(x => new SimpleStudyGroupDto(x.Id, x.Title, x.CreateDate)), 
               result.Count);
        }
    }
}
