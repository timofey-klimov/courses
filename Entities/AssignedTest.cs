using Entities.Base;
using System;

namespace Entities
{
    public class AssignedTest : AuditableEntity<int>
    {
        public Test Test { get; private set; }

        public DateTime Deadline { get; private set; }

        public DateTime? CompletedDate { get; set; }

        public AssignedTest(Test test, DateTime deadLine)
        {
            Test = test;
            Deadline = deadLine;
        }

        private AssignedTest() { }
    }
}
