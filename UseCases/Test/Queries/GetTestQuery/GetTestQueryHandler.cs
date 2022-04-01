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
using UseCases.Test.Exceptions;
using UseCases.Test.Services.Abstract;

namespace UseCases.Test.Queries.GetTestQuery
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQueryRequest, TestWithQuestionsDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITestMapService _mapper;
        public GetTestQueryHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider, ITestMapService mapper)
        {
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
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
                    Test = teacher.GetCreatedTest(request.Id)
                })
               .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());

            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            return _mapper.MapFromTestToTestWithQuestionDto(result.Test);
        }
    }
}
