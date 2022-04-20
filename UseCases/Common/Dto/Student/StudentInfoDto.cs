using System.Collections.Generic;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;

namespace UseCases.Teachers.Dto
{
    public class StudentInfoDto
    {
        public StudentDto StudentDto { get; }

        public IEnumerable<AssignedTestDto> AssignTests { get; }

        public IEnumerable<SimpleStudyGroupDto> StudyGroups { get; }

        public StudentInfoDto(
            StudentDto studentDto, 
            IEnumerable<AssignedTestDto> assignTests, 
            IEnumerable<SimpleStudyGroupDto> studyGroups
            )
        {
            StudentDto = studentDto;
            AssignTests = assignTests;
            StudyGroups = studyGroups;
        }
    }
}
