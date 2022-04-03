using MediatR;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Queries.GetTeachersQuery
{
    public record GetTeachersRequest(string Name, string Surname, int Offset, int Limit) : IRequest<Pagination<TeacherDto>>;
}
