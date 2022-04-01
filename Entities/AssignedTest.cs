using Entities.Base;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class AssignedTest : Entity<int>
    {
        public Test Test { get; private set; }

        public DateTime Deadline { get; private set; }

        public DateTime CreateDate { get; private set; }


        private List<StudentAssignTest> _studentAssignTest;
        public IReadOnlyCollection<StudentAssignTest> StudentAssignTests => _studentAssignTest;

        public AssignedTest(Test test, DateTime deadLine)
        {
            Test = test;
            Deadline = deadLine;
            CreateDate = DateTime.Now;
        }

        private AssignedTest() { }
    }
}
