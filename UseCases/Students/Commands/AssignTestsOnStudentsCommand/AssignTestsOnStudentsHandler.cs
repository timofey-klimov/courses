using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Exceptions;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
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
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                .Include(x => x.StudentTeachers)
                    .ThenInclude(x => x.Student)
                .Select(x => new
                {
                    TeacherId = x.Id,
                    Test = x.CreatedTests.FirstOrDefault(x =>x.Id == request.TestsId),
                    Students = x.StudentTeachers
                        .Select(x => x.Student)
                        .Where(x => request.StudentsId.Contains(x.Id))
                })
                .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());


            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            var assignTest = new AssignedTest(result.Test, request.Deadline);

            foreach (var student in result.Students)
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
