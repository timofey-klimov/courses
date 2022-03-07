using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Test.Dto;

namespace UseCases.Test.CreateTest
{
    public class CreateTestRequestHandler : IRequestHandler<CreateTestRequest>
    {

        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public CreateTestRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Unit> Handle(CreateTestRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (!_currentUserProvider.IsAdmin())
                throw new AccessDeniedException();

            var requestQuetions = request.Questions ?? new List<QuestionDto>();

            var questions = new List<Question>(requestQuetions.Count);

            foreach (var question in requestQuetions)
            {
                switch (question.Type)
                {
                    case Dto.QuestionTypeDto.WithTextInput:
                        questions.Add(new QuestionWithTextAnswer(question.Title, question.Content));
                        break;
                    case Dto.QuestionTypeDto.WithFileInput:
                        questions.Add(new QuestionWithFileAnswer(question.Title, question.Content));
                        break;
                    case Dto.QuestionTypeDto.WithAnswerOptions:
                        questions.Add(new QuestionWithAnswerOptions(question.Title, question.Content, question.AnswerOptions.Select(x => new AnswerOption(x.Content, x.IsCorrect))));
                        break;
                }
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            var test = new Entities.Test(user, request.Title, questions);

            _dbContext.Tests.Add(test);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
