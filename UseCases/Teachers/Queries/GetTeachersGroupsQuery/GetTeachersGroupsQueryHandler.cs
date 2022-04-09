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
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetTeachersGroupsQuery
{
    public class GetTeachersGroupsQueryHandler : IRequestHandler<GetTeachersGroupsQueryRequest, Pagination<StudyGroupWithStudentCount>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetTeachersGroupsQueryHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<StudyGroupWithStudentCount>> Handle(GetTeachersGroupsQueryRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var spec = new StudyGroupFilterSpecification(request.Title, request.startDate, request.endDate);

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.Students)
                .Select(x => new
                {
                    Id = x.Id,
                    Groups = x.StudyGroups
                        .AsQueryable()
                        .Where(spec.CreateCriteria())
                        .Select(x => new
                        {
                            GroupInfo = x,
                            StudentsCount = x.Students.Count()
                        })
                        .Skip(request.offset)
                        .Take(request.limit)
                        .AsEnumerable(),
                    Count = x.StudyGroups
                        .AsQueryable()
                        .Where(spec.CreateCriteria())
                        .Count()
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (result == null)
                throw new ParticipantNotFoundException();
    
           return new Pagination<StudyGroupWithStudentCount>(
               result.Groups.Select(
                   x => new StudyGroupWithStudentCount(x.GroupInfo.Id, x.GroupInfo.Title, x.GroupInfo.CreateDate, x.StudentsCount)), 
               result.Count);
        }
    }
}
