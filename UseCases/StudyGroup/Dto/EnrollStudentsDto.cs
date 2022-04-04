using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Dto
{
    public record EnrollStudentsDto(SimpleStudyGroupDto Group, IEnumerable<StudentDto> Students);
   
}
