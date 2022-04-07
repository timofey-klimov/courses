using Authorization.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.StudyGroup;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetTeachersGroupsQuery
{
    public class GetTeachersGroupsQueryHandler : IRequestHandler<GetTeachersGroupsQueryRequest, Pagination<TeacherStudyGroupDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IFilterProvider _filterProvider;

        public GetTeachersGroupsQueryHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider,IFilterProvider filterProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _filterProvider = filterProvider ?? throw new ArgumentNullException(nameof(filterProvider));
        }
        public async Task<Pagination<TeacherStudyGroupDto>> Handle(GetTeachersGroupsQueryRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var spec = new StudyGroupFilterSpecification(request.Title, request.startDate, request.endDate);

            var baseQuery = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                .Select(x => new
                {
                    Id = x.Id,
                    Groups = _filterProvider.GetQuery(x.StudyGroups.AsQueryable(), spec)
                        .Skip(request.offset)
                        .Take(request.limit)
                        .AsEnumerable(),
                    Count = _filterProvider.GetQuery(x.StudyGroups.AsQueryable(), spec)
                        .Skip(request.offset)
                        .Take(request.limit)
                        .Count()
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());


            return default;
           // return new Pagination<TeacherStudyGroupDto>(groupsDto, count);
        }
    }
}
