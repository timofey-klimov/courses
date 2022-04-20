using System;
using System.Collections.Generic;
using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Dto
{
    public record StudyGroupInfoDto(string Title, DateTime CreateDate, TeacherDto Teacher, IEnumerable<StudentDto> Students);
    
}
