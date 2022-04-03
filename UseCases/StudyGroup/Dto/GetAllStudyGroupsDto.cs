using System;
using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Dto
{
    public record GetAllStudyGroupsDto(int Id, string Title, TeacherDto Teacher, DateTime CreateDate);
   
}
