using MediatR;
using System.Collections.Generic;
using UseCases.Test.Dto;
using UseCases.Test.Dto.Request;

namespace UseCases.Test.CreateTest
{
    public record CreateTestRequest(string Title, ICollection<CreateQuestionDto> Questions) 
        : IRequest<TestDto>;
}
