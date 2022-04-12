using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Exceptions;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Students.Exceptions;
using UseCases.Test.Exceptions;

namespace UseCases.Students.Commands.AssignTestsOnStudentsCommand
{
    public class AssignTestsOnStudentsHandler : IRequestHandler<AssignTestsOnStudentsRequest, AssignTestDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<AssignTestsOnStudentsHandler> _logger;
        public AssignTestsOnStudentsHandler(IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider, 
            ILogger<AssignTestsOnStudentsHandler> logger)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
            _logger = logger;
        }
        public async Task<AssignTestDto> Handle(AssignTestsOnStudentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.CreatedTests)
                .Select(x => new
                {
                    TeacherId = x.Id,
                    Test = x.CreatedTests.FirstOrDefault(x =>x.Id == request.testsId)                
                })
                .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());



            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            var students = _dbContext.Participants
                .OfType<Student>()
                .Include(x => x.StudentStudyGroups)
                    .ThenInclude(x => x.StudyGroup)
                .Include(x => x.StudentAssignedTests)
                    .ThenInclude(x => x.AssignedTest)
                .Where(x => request.studentsId.Contains(x.Id) && x.StudentStudyGroups
                    .Select(y => y.StudyGroup)
                    .Any(z => z.Teacher.Id == result.TeacherId))
                    .AsEnumerable();

            if (!students.Any())
                throw new StudentNotFoundException();

            var assignTest = new AssignedTest(result.Test, request.deadline);

            foreach (var student in students)
            {
                try
                {
                    student.AssignTest(assignTest);
                }
                catch (TestAlreadyAssignedException ex)
                {
                    _logger.LogError(ex.GetType().Name);
                }
            }
            await _dbContext.SaveChangesAsync();

            return new AssignTestDto(result.Test.Title, assignTest.CreateDate, assignTest.Deadline);

        }
    }
}
