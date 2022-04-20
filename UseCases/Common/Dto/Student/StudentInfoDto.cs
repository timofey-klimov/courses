using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Dto
{
    public class StudentInfoDto
    {
        public StudentDto StudentDto { get; }

        public IEnumerable<AssignedTestDto> AssignTests { get; }

        public StudentInfoDto(StudentDto studentDto, IEnumerable<AssignedTestDto> assignTests)
        {
            StudentDto = studentDto;
            AssignTests = assignTests;
        }
    }
}
