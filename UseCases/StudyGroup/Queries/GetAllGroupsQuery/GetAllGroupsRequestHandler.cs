using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetAllGroupsQuery
{
    public class GetAllGroupsRequestHandler : IRequestHandler<GetAllGroupsRequest, Pagination<StudyGroupDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetAllGroupsRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<StudyGroupDto>> Handle(GetAllGroupsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var query = _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.Teacher)
                .Where(x => x.CreatedBy == _currentUserProvider.GetUserId());
                
            var totalCount = await query.SelectMany(x => x.StudyGroups).CountAsync();

            var result = query.SelectMany(x => x.StudyGroups)
                .Skip(request.Offset)
                .Take(request.Limit);
                

            var list = new List<StudyGroupDto>(result.Count());
                
            foreach (var item in result)
            {
                list.Add(new StudyGroupDto
                    (item.Id, item.Title, 
                    new TeacherDto(item.Teacher.Id, item.Teacher.Name, item.Teacher.Surname, item.Teacher.Login), item.CreateDate));
            }

            return new Pagination<StudyGroupDto>(list, totalCount);
        }
    }
}
