using MediatR;
using System.Collections.Generic;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.CreateStudyGroupCommand
{
    public record CreateStudyGroupRequest(string Title, IEnumerable<int> StudentsIds, int TeacherId) 
        : IRequest<StudyGroupDto>;
    
}
