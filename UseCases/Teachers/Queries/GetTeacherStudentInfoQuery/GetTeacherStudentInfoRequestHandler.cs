using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;
using UseCases.Common.Services.Abstract.Mapper;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetTeacherStudentInfoQuery
{
    public class GetTeacherStudentInfoRequestHandler : IRequestHandler<GetTeacherStudentInfoRequest, StudentInfoDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IStudentMapper _studentMapper;

        public GetTeacherStudentInfoRequestHandler(
            IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider,
            IStudentMapper studentMapper
            )
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _studentMapper = studentMapper ?? throw new ArgumentNullException(nameof(studentMapper));
        }

        public async Task<StudentInfoDto> Handle(GetTeacherStudentInfoRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudentTeachers)
                    .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.StudentAssignedTests)
                            .ThenInclude(x => x.AssignedTest)
                                .ThenInclude(x => x.Test)
                .Include(x => x.StudyGroups)
                .Select(teacher => new
                {
                    Id = teacher.Id,
                    StudentInfo = teacher.StudentTeachers
                        .Select(x => x.Student)
                        .Select(student => new
                        {
                            Student = student,
                            Groups = teacher.StudyGroups
                                .Where(x => x.Students.Any(x => x.StudentId == student.Id))
                        })
                        .FirstOrDefault(x => x.Student.Id == request.StudentId)
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (result == null || result.StudentInfo == null)
                throw new ParticipantNotFoundException();

            return _studentMapper
                .ToStudentInfo(
                    result.StudentInfo.Student, result.StudentInfo.Groups);
        }
    }
}
