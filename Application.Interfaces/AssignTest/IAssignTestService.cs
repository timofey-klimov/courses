using Entities;

namespace Application.Interfaces.AssignTest
{
    public interface IAssignTestService
    {
        bool CheckIfAssignedTestOverdue(AssignedTest assignedTest);

        bool ChecckIfAssignedTestOverdueSoon(AssignedTest assignedTest);
    }
}
