using MediatR;
using System;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Students.Commands.AssignTestsOnStudentsCommand
{
    public record AssignTestsOnStudentsRequest(int TestsId, IEnumerable<int> StudentsId, DateTime Deadline) 
        : IRequest<AssignedTestDto>;
}
