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
    public class GetAllGroupsRequestHandler : IRequestHandler<GetAllGroupsRequest, IEnumerable<GetAllStudyGroupsDto>>
    {
        private readonly IDbContext _dbContext;

        public GetAllGroupsRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<GetAllStudyGroupsDto>> Handle(GetAllGroupsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                .Select(x => new
                {
                    Teacher = x,
                    Groups = x.StudyGroups
                })
                .AsEnumerable();

            var list = new List<GetAllStudyGroupsDto>(result.SelectMany(x => x.Groups).Count());
                
            foreach (var item in result)
            {
                var teacher = item.Teacher;

                foreach (var group in item.Groups)
                {
                    list.Add(new GetAllStudyGroupsDto
                        (group.Id, group.Title, new TeacherDto(teacher.Id, teacher.Name, teacher.Surname, teacher.Login), group.CreateDate));
                }
            }

            return list;

        }
    }
}
