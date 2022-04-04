using MediatR;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Students.Queries.GetStudentsQuery
{
    public record GetStudentsRequest(int Offset, int Limit) : IRequest<Pagination<StudentDto>>;
   
}
