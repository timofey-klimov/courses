using System;

namespace UseCases.Common.Dto
{
    public record AssignedTestDto(int Id,string Title, DateTime CreateDate, DateTime Deadline, bool overDueSoon, bool overDue);
}
