using System.Collections.Generic;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;

namespace UseCases.Teachers.Dto
{
    public record StudyGroupFullInfoDto(IEnumerable<StudentDto> Students, IEnumerable<AssignedTestDto> AssignTests, SimpleStudyGroupDto studyGroup);
   
}
