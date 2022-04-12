using Entities.Base;

namespace Entities.Participants
{
    public class StudentTeacher : AuditableEntity<int>
    {
        public int TeacherId { get; private set; }

        public int StudentId { get; private set; }

        public Teacher Teacher { get; private set; }

        public Student Student { get; private set; }

        public StudentTeacher(Teacher teacher, Student student)
        {
            Teacher = teacher;
            Student = student;
        }

        private StudentTeacher() { }
    }
}
