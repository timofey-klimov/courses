using MediatR;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetTestQuery
{
    public record GetTestRequest(int Id) : IRequest<TestWithQuestionsDto>;
}
