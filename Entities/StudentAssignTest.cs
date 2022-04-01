using Entities.Base;
using Entities.Participants;

namespace Entities
{
    public class StudentAssignTest : AuditableEntity<int>
    {
        public int StudentId { get; private set; }

        public Student Student { get; private set; }

        public int AssignTestId { get; private set; }

        public AssignedTest AssignedTest { get; private set; }

        public StudentAssignTest(Student student, AssignedTest assignedTest)
        {
            Student = student;
            AssignedTest = assignedTest;
        }

        private StudentAssignTest() { }
    }
}
