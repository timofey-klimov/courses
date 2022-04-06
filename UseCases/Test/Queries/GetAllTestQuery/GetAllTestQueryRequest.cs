using MediatR;
using System.Collections.Generic;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetAllTestQuery
{
    public record GetAllTestQueryRequest(int TeacherId) : IRequest<IEnumerable<TestDto>>;
}
