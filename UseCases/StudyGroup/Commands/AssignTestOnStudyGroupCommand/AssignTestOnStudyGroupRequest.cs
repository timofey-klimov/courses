using MediatR;
using System;
using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand
{
    public record AssignTestOnStudyGroupRequest(int GroupId, int TestId, DateTime Deadline)
        : IRequest<AssignedTestDto>;
  
}
