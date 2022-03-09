using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Users;
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

            if (!_currentUserProvider.IsManager())
                throw new AccessDeniedException();

            var requestQuetions = request.Questions ?? new List<QuestionDto>();

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

            var createdManager = await _dbContext.Participants
                .OfType<Manager>()
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            var test = new Entities.Test(createdManager, request.Title, questions);

            _dbContext.Tests.Add(test);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
