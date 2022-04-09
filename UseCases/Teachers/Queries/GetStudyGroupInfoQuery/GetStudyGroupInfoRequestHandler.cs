using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities.Exceptions;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetStudyGroupInfoQuery
{
    public class GetStudyGroupInfoRequestHandler : IRequestHandler<GetStudyGroupInfoRequest, StudyGroupFullInfoDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetStudyGroupInfoRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<StudyGroupFullInfoDto> Handle(GetStudyGroupInfoRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.Students)
                        .ThenInclude(x => x.Student)
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.AssignedTests)
                        .ThenInclude(x => x.Test)
                .Select(x => new
                {
                    TeacherId = x.Id,
                    GroupInfo = x.StudyGroups
                        .Select(x => new
                        {
                            GroupId = x.Id,
                            Title = x.Title,
                            CreateDate = x.CreateDate,
                            StudentsInfo = x.Students.Select(x => new
                            {
                                Name = x.Student.Name,
                                Surname = x.Student.Surname,
                                Login = x.Student.Login
                            }),
                            AssignTestsInfo = x.AssignedTests.Select(x => new
                            {
                                Title = x.Test.Title,
                                CreateDate = x.CreateDate,
                                Deadline = x.Deadline
                            }),
                        })
                        .FirstOrDefault(x => x.GroupId == request.Id),
                })
                .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());

            if (result == null || result.GroupInfo == null)
                throw new GroupNotFoundException();

            return new StudyGroupFullInfoDto(result.GroupInfo.StudentsInfo.Select(
                    x => new Common.Dto.StudentDto(default, x.Name, x.Surname, x.Login)),
                result.GroupInfo.AssignTestsInfo.Select(
                    x => new Common.Dto.AssignTestDto(x.Title, x.CreateDate, x.Deadline)),
                new StudyGroup.Dto.SimpleStudyGroupDto(result.GroupInfo.GroupId, result.GroupInfo.Title, result.GroupInfo.CreateDate));
        }
    }
}
