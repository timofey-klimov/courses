using MediatR;
using System.Collections.Generic;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.EnrollStudentsInGroupCommand
{
    public record EnrollStudentsInGroupRequest(int GroupId, IEnumerable<int> Students) 
        : IRequest<EnrollStudentsDto>;
    
}
