using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Dto
{
    public record EnrollStudentsDto(StudyGroupDto Group, IEnumerable<StudentDto> Students);
   
}
