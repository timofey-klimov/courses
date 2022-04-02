using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.AssignTest.Dto;
using UseCases.Common.Participant;

namespace UseCases.AssignTest.Queries.GetAssignedOnStudentTestsQuery
{
    public class GetAssignedOnStudentTestRequestHandler : IRequestHandler<GetAssignedOnStudentTestsRequest, IEnumerable<GetAssignedTestDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetAssignedOnStudentTestRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<IEnumerable<GetAssignedTestDto>> Handle(GetAssignedOnStudentTestsRequest request, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Participants
                .OfType<Student>()
                .Include(x => x.StudentAssignedTests)
                    .ThenInclude(x => x.AssignedTest)
                        .ThenInclude(x => x.Test)
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (student == null)
                throw new ParticipantNotFoundException();


            return student.AssignedTests()
                .Select(x =>
                {
                    var deadlineIsSoon = DateTime.Now < x.Deadline && DateTime.Now.Subtract(x.Deadline).TotalDays >= 2;
                    return new GetAssignedTestDto(x.Id, x.Test.Title, x.Deadline, deadlineIsSoon, DateTime.Now > x.Deadline);
                });
        }
    }
}
