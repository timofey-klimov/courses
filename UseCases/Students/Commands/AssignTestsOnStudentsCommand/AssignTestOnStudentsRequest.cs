using MediatR;
using System;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Students.Commands.AssignTestsOnStudentsCommand
{
    public record AssignTestsOnStudentsRequest(int testsId, IEnumerable<int> studentsId, DateTime deadline) : IRequest<AssignTestDto>;
}
