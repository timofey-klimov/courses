using MediatR;
using System.Collections.Generic;
using UseCases.Test.Dto;

namespace UseCases.Test.CreateTest
{
    public class CreateTestRequest : IRequest<TestDto>
    {
        public string Title { get; }
        public ICollection<QuestionDto> Questions { get; }

        public CreateTestRequest(ICollection<QuestionDto> questions, string title)
        {
            Questions = questions;
            Title = title;
        }
    }
}
