using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.Dto;
using UseCases.Test.Exceptions;
using UseCases.Test.Services.Abstract;

namespace UseCases.Test.Queries.GetTestQuery
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQueryRequest, TestWithQuestionsDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ITestMapService _mapper;
        public GetTestQueryHandler(IDbContext dbContext, ITestMapService mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<TestWithQuestionsDto> Handle(GetTestQueryRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                    .ThenInclude(x => x.Questions)
                        .ThenInclude(x => (x as QuestionWithAnswerOptions).Answers)
                .Select(teacher => new
                {
                    TeacherId = teacher.Id,
                    Test = teacher.CreatedTests.FirstOrDefault(x => x.Id == request.TestId)
                })
               .FirstOrDefaultAsync(x => x.TeacherId == request.TeacherId);

            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            return _mapper.MapFromTestToTestWithQuestionDto(result.Test);
        }
    }
}
