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


        public Student AssignTest(Test test, DateTime deadline)
        {
            if (_tests.Any(x => x.Test.Equals(test)))
                throw new TestAlreadyAssignedException();

            var assignedTest = new AssignedTest(test, deadline);

            _tests.Add(assignedTest);

            return this;
        }
    }
}
