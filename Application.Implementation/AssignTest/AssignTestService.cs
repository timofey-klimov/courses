using Application.Interfaces.AssignTest;
using Entities;
using System;

namespace Application.Implementation.AssignTest
{
    public class AssignTestService : IAssignTestService
    {
        public bool ChecckIfAssignedTestOverdueSoon(AssignedTest assignedTest)
        {
            return !CheckIfAssignedTestOverdue(assignedTest) 
                && DateTime.Now.AddDays(2) > assignedTest.Deadline;
        }

        public bool CheckIfAssignedTestOverdue(AssignedTest assignedTest)
        {
            return assignedTest.Deadline < DateTime.Now;
        }
    }
}
