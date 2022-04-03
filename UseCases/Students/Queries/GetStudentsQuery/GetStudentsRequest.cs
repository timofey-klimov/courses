using MediatR;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Students.Queries.GetStudentsQuery
{
    public record GetStudentsRequest() : IRequest<IEnumerable<StudentDto>>;
   
}
