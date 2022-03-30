using Entities;
using System;
using System.Linq;
using UseCases.Test.Dto;
using UseCases.Test.Services.Abstract;

namespace UseCases.Test.Services.Implementation
{
    public class TestMapService : ITestMapService
    {
        public TestWithQuestionsDto MapFromTestToTestWithQuestionDto(Entities.Test test)
        {
            return new TestWithQuestionsDto()
            {
                Id = test.Id,
                CreateDate = test.CreateDate,
                Title = test.Title,
                Questions = test.Questions.Select(x => new QuestionDto()
                {
                    Id = x.Id,
                    Content = x.Content,
                    Position = x.Position,
                    Type = MapType(x.GetType()),
                    AnswerOptions = (x as QuestionWithAnswerOptions)?.Answers.Select(x => new AsnwerOptionDto() { Id = x.Id, Content = x.Content, IsCorreсt = x.IsCorrect})
                })
            };
        }


        private QuestionTypeDto MapType(Type questionType)
        {
            if (questionType == typeof(QuestionWithAnswerOptions))
                return QuestionTypeDto.WithAnswerOptions;

            if (questionType == typeof(QuestionWithFileAnswer))
                return QuestionTypeDto.WithFileInput;

            if (questionType == typeof(QuestionWithTextAnswer))
                return QuestionTypeDto.WithTextInput;

            throw new Exception("No such type");
        }
    }
}
