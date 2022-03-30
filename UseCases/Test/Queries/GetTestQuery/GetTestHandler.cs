using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetTestQuery
{
    public class GetTestHandler : IRequestHandler<GetTestRequest, TestWithQuestionsDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetTestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _dbContext = dbContext;
        }
        public async Task<TestWithQuestionsDto> Handle(GetTestRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                .ThenInclude(x=>x.Questions)
                .ThenInclude(x=>(x as QuestionWithAnswerOptions).Answers)
                .Select(teacher => new
                {
                    TeacherId = teacher.Id,
                    Test = teacher.GetTeacherTest(request.Id)
                })
                .FirstOrDefaultAsync(x=>x.TeacherId == _currentUserProvider.GetUserId());
            //var questions = result.Test.
            
            
            return new TestWithQuestionsDto() {};
        }
    }
}
