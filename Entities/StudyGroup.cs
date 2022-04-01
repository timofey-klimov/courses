using Entities.Base;
using Entities.Exceptions;
using Entities.Participants;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class StudyGroup : AuditableEntity<int>
    {
        public string Title { get; private set; }

        private List<StudentStudyGroup> _students;
        public IReadOnlyCollection<StudentStudyGroup> Students => _students;

        public StudyGroup(string title, IEnumerable<Student> students)
        {
            if (!students?.Any() == true)
                throw new GroupCantBeEmptyException();

            Title = title;
            _students = students
                .Select(x => new StudentStudyGroup(x, this))
                .ToList();
        }

        private StudyGroup() { }


        public IReadOnlyCollection<Student> GetEnrolledStudents() =>
            _students?.Where(x => x.StudyGroup == this)?.Select(x => x.Student)?.ToList();
    }
}
