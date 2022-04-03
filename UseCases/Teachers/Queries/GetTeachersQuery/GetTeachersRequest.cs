using MediatR;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Queries.GetTeachersQuery
{
    public record GetTeachersRequest(string Name, string Surname) : IRequest<IEnumerable<TeacherDto>>;
}
