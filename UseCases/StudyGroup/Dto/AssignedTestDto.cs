using System;

namespace UseCases.StudyGroup.Dto
{
    public record AssignedTestDto(int Id, DateTime CreateDate, DateTime Deadline, string Title);
    
}
