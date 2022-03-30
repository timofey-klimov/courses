using Entities.Base;
using Entities.Participants;

namespace Entities
{
    public class StudentStudyGroup : AuditableEntity<int>
    {
        public int StudentId { get; private set; }

        public Student Student { get; private set; }

        public int StudyGroupId { get; private set; }

        public StudyGroup StudyGroup { get; private set; }

        public StudentStudyGroup(Student student, StudyGroup studyGroup)
        {
            Student = student;
            StudyGroup = studyGroup;
        }

        private StudentStudyGroup() { }
    }
}
