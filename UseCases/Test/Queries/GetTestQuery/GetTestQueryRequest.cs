using MediatR;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetTestQuery
{
    public record GetTestQueryRequest(int Id) : IRequest<TestWithQuestionsDto>;
}
