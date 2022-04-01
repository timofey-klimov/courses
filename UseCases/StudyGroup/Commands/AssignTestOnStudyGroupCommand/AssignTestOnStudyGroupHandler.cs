using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;
using UseCases.Test.Exceptions;

namespace UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand
{
    public class AssignTestOnStudyGroupHandler : IRequestHandler<AssignTestOnStudyGroupRequest, AssignedTestDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<AssignTestOnStudyGroupHandler> _logger;

        public AssignTestOnStudyGroupHandler(
            IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider,
            ILogger<AssignTestOnStudyGroupHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AssignedTestDto> Handle(AssignTestOnStudyGroupRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.CreatedTests)
                .Select(x => new
                {
                    TeacherId = x.Id,
                    Test = x.GetCreatedTest(request.TestId)
                })
                .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());

            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            var group = await _dbContext.StudyGroups
                .Include(x => x.Students)
                    .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.StudentAssignedTests)
                .Include(x => x.Students)
                    .ThenInclude(x => x.StudyGroup)
                .Select(x => new
                {
                    GroupId = x.Id,
                    Students = x.GetEnrolledStudents()
                })
                .FirstOrDefaultAsync(x => x.GroupId == request.GroupId);

            if (group == null || group.Students == null)
                throw new GroupNotFoundException();

            var assignTest = new AssignedTest(result.Test, request.Deadline);

            foreach (var student in group.Students)
            {
                try
                {
                    student.AssignTest(assignTest);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            await _dbContext.SaveChangesAsync();

            return new AssignedTestDto(assignTest.Id, assignTest.CreateDate, assignTest.Deadline, result.Test.Title);
        }
    }
}
