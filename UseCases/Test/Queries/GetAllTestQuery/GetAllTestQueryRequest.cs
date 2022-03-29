using MediatR;
using System.Collections.Generic;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetAllTestQuery
{
    public record GetAllTestQueryRequest : IRequest<IEnumerable<TestDto>>;
}
