using System;

namespace UseCases.AssignTest.Dto
{
    public record GetAssignedTestDto(int Id, string Title, DateTime Deadline, bool deadlineIsSoon, bool overDue);
    
}
