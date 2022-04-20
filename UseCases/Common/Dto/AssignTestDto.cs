using System;

namespace UseCases.Common.Dto
{
    public record AssignTestDto(int Id,string Title, DateTime CreateDate, DateTime Deadline, bool overDueSoon, bool overDue);
}
