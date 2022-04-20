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
using UseCases.Common.Services.Abstract.Mapper;
using UseCases.Test.Exceptions;

namespace UseCases.Students.Commands.AssignTestsOnStudentsCommand
{
    public class AssignTestsOnStudentsHandler : IRequestHandler<AssignTestsOnStudentsRequest, AssignedTestDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<AssignTestsOnStudentsHandler> _logger;
        private readonly IAssignTestMapper _mapper;

        public AssignTestsOnStudentsHandler(IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider, 
            ILogger<AssignTestsOnStudentsHandler> logger,
            IAssignTestMapper assignTestMapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = assignTestMapper ?? throw new ArgumentNullException(nameof(assignTestMapper));
        }

        public async Task<AssignedTestDto> Handle(AssignTestsOnStudentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                .Include(x => x.StudentTeachers)
                    .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.StudentAssignedTests)
                            .ThenInclude(x => x.AssignedTest)
                                .ThenInclude(x => x.Test)
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

            return _mapper.ToAssignTestDto(assignTest);
        }
    }
}
