using UseCases.Common.Dto;

namespace UseCases.StudyGroup.Dto
{
    public record CreateStudyGroupDto(StudyGroupDto studyGroup, TeacherDto teacherDto);
}
