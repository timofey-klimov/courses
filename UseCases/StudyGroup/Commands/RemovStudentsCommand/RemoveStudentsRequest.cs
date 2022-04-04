using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.RemovStudentsCommand
{
    public record RemoveStudentsRequest(int studyGroupId, IEnumerable<int> studentsId) : IRequest<RemoveStudentsFromStudyGroupDto>;
}
