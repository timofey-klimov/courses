using DataAccess.Interfaces;
using Entities.Exceptions;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetStudyGroupInfoQuery
{
    public class GetStudyGroupInfoRequestHandler : IRequestHandler<GetStudyGroupInfoRequest, StudyGroupInfoDto>
    {
        private readonly IDbContext _dbContext;
        public GetStudyGroupInfoRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<StudyGroupInfoDto> Handle(GetStudyGroupInfoRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.Students)
                        .ThenInclude(x => x.Student)
                .Select(x => new
                {
                    Id = x.Id,
                    Teacher = new {x.Id, x.Name, x.Surname, x.Login},
                    StudyGroup = x.StudyGroups
                        .Select(x => new
                        {
                            Id = x.Id,
                            CreateDate = x.CreateDate,
                            Title = x.Title,
                            Students = x.Students.Select(x => new
                            {
                                Id = x.Student.Id,
                                Name = x.Student.Name,
                                Surname = x.Student.Surname,
                                Login = x.Student.Login
                            })
                        })
                        .FirstOrDefault(x => x.Id == request.GroupId),
                })
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId);

            if (result == null || result.StudyGroup == null)
                throw new GroupNotFoundException();

            return new StudyGroupInfoDto(result.StudyGroup.Title,
                result.StudyGroup.CreateDate,
                new Common.Dto.TeacherDto(result.Teacher.Id, result.Teacher.Name, result.Teacher.Surname, result.Teacher.Login),
                result.StudyGroup.Students.Select(x => new Common.Dto.StudentDto(x.Id, x.Name, x.Surname, x.Login)));
        }
    }
}
