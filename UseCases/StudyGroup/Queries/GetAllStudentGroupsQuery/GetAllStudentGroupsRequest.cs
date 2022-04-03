using MediatR;
using System.Collections.Generic;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetAllStudentGroups
{
    public record GetAllStudentGroupsRequest(int StudentId, int TeacherId) : IRequest<IEnumerable<StudyGroupDto>>;
}
