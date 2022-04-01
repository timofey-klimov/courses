using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetAllStudentGroups
{
    public record GetAllStudentGroupsRequest(int StudentId) : IRequest<IEnumerable<StudyGroupDto>>;
}
