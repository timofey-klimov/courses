using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.Dto;
using UseCases.Test.Dto.Request;

namespace UseCases.Test.CreateTest
{
    public class CreateTestRequestHandler : IRequestHandler<CreateTestRequest, TestDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public CreateTestRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<TestDto> Handle(CreateTestRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var requestQuetions = request.Questions ?? new List<CreateQuestionDto>();

            var questions = new List<Question>(requestQuetions.Count);

            foreach (var question in requestQuetions)
            {
                switch (question.Type)
                {
                    case QuestionTypeDto.WithTextInput:
                        questions.Add(new QuestionWithTextAnswer(question.Content, question.Position));
                        break;
                    case QuestionTypeDto.WithFileInput:
                        questions.Add(new QuestionWithFileAnswer(question.Content, question.Position));
                        break;
                    case QuestionTypeDto.WithAnswerOptions:
                        questions.Add(new QuestionWithAnswerOptions(question.Content, question.Position, 
                            question.AnswerOptions.Select(x => new AnswerOption(x.Content, x.IsCorreсt))));
                        break;
                }
            }

            var test = new Entities.Test(request.Title, questions);

            var teacher = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            teacher.CreateNewTest(test);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new TestDto() { Id = test.Id, Title = test.Title, CreateDate = test.CreateDate};
        }
    }
}
