using MediatR;
using UseCases.Common.Dto;
using UseCases.Test.Dto;

namespace UseCases.Teachers.Queries.GetTeachersTestsQuery
{
    public record GetTeachersTestsQueryRequest(int Offset, int Limit) : IRequest<Pagination<TestDto>>;
    
}
