using Entities.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Participants
{
    public class Teacher : Participant
    {
        public Teacher(string login, string name, string surname, string password, string hashedPassword, ParticipantRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
            _tests = new List<Test>();
            _groups = new List<StudyGroup>();
        }

        protected Teacher()
        {
        }

        private List<Test> _tests;
        public IReadOnlyCollection<Test> CreatedTests => _tests;

        private List<StudyGroup> _groups;
        public IReadOnlyCollection<StudyGroup> StudyGroups => _groups;


        public void CreateNewTest(Test test)
        {
            if (_tests.Any(x => x.Title == test.Title))
                throw new TestAlreadyExistException();

            _tests.Add(test);
        }

        public Test GetCreatedTest(int id)
        {
            return _tests.FirstOrDefault(x => x.Id == id);
        }

        public void AssignGroup(StudyGroup studyGroup)
        {
            if (_groups.Contains(studyGroup))
                throw new GroupAlreadyAssignedException();

            _groups.Add(studyGroup);
        }
    }
}
