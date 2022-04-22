using Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Participants
{
    public class Student : Participant
    {
        protected Student() { }
        public Student(string login, string name, string surname, string password, string hashedPassword, Avatar avatar, ParticipantRole userRole) 
            : base(login, name, surname, password, hashedPassword, avatar, userRole)
        {
            _tests = new List<StudentAssignTest>();
            _groups = new List<StudentStudyGroup>();
            _studentTeachers = new List<StudentTeacher>();
        }

        private List<StudentAssignTest> _tests;
        public IReadOnlyCollection<StudentAssignTest> StudentAssignedTests => _tests;


        private List<StudentStudyGroup> _groups;
        public IReadOnlyCollection<StudentStudyGroup> StudentStudyGroups => _groups;

        private List<StudentTeacher> _studentTeachers;

        public IReadOnlyCollection<StudentTeacher> StudentTeachers => _studentTeachers;


        public Student AssignTest(AssignedTest assignedTest)
        {
            if (_tests.Any(x => x.AssignedTest.Test == assignedTest.Test 
                    && x.AssignedTest.Deadline == assignedTest.Deadline))
                throw new TestAlreadyAssignedException();

            _tests.Add(new StudentAssignTest(this, assignedTest));

            return this;
        }

        public Student Enroll(StudyGroup studyGroup)
        {
            if (_groups.Any(x => x.Student == this && x.StudyGroup == studyGroup))
                throw new StudentAlreadyEnrollException();

            _groups.Add(new StudentStudyGroup(this, studyGroup));

            return this;
        }

        public IReadOnlyCollection<StudyGroup> EnrolledGroups() 
            => this.StudentStudyGroups?.Select(x => x.StudyGroup)?.ToList();

        public IReadOnlyCollection<AssignedTest> AssignedTests()
            => this.StudentAssignedTests?.Select(x => x.AssignedTest)?.ToList();

        public IReadOnlyCollection<Teacher> GetAllTeachers()
            => this.StudentTeachers?.Select(x => x.Teacher)?.ToList();
    }
}
