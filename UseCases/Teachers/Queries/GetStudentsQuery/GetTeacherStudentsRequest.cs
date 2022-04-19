using MediatR;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Queries.GetStudentsQuery
{
    public record GetTeacherStudentsRequest(int Offset, int Limit)
        : IRequest<Pagination<StudentDto>>;
   
}
