using MediatR;
using System.Collections.Generic;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.RemovStudentsCommand
{
    public record RemoveStudentsRequest(int StudyGroupId, IEnumerable<int> StudentsId) : IRequest<RemoveStudentsFromStudyGroupDto>;
}
