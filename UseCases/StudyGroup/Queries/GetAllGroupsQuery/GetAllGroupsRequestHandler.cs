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
    public class GetAllGroupsRequestHandler : IRequestHandler<GetAllGroupsRequest, Pagination<GetAllStudyGroupsDto>>
    {
        private readonly IDbContext _dbContext;

        public GetAllGroupsRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Pagination<GetAllStudyGroupsDto>> Handle(GetAllGroupsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.StudyGroups)
                .Select(x => new
                {
                    Teacher = x,
                    Groups = x.StudyGroups
                })
                .Skip(request.Offset)
                .Take(request.Limit)
                .AsEnumerable();

            var count = result.SelectMany(x => x.Groups).Count();

            var list = new List<GetAllStudyGroupsDto>(count);
                
            foreach (var item in result)
            {
                var teacher = item.Teacher;

                foreach (var group in item.Groups)
                {
                    list.Add(new GetAllStudyGroupsDto
                        (group.Id, group.Title, new TeacherDto(teacher.Id, teacher.Name, teacher.Surname, teacher.Login), group.CreateDate));
                }
            }

            return new Pagination<GetAllStudyGroupsDto>(list, count);
        }
    }
}
