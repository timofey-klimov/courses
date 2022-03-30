using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Participants
{
    public class Student : Participant
    {
        protected Student() { }
        public Student(string login, string name, string surname, string password, string hashedPassword, ParticipantRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
        }

        private List<AssignedTest> _tests;
        public IReadOnlyCollection<AssignedTest> AssignedTests => _tests;


        private List<StudentStudyGroup> _groups;
        public IReadOnlyCollection<StudentStudyGroup> StudyGroups => _groups;


        public Student AssignTest(Test test, DateTime deadline)
        {
            if (_tests.Any(x => x.Test == test))
                throw new TestAlreadyAssignedException();

            var assignedTest = new AssignedTest(test, deadline);

            _tests.Add(assignedTest);

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
            => this.StudyGroups?.Where(x => x.Student == this)?.Select(x => x.StudyGroup)?.ToList();
    }
}
