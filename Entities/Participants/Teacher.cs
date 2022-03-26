using System.Collections.Generic;

namespace Entities.Participants
{
    public class Teacher : Participant
    {
        public Teacher(string login, string name, string surname, string password, string hashedPassword, ParticipantRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
            _tests = new List<Test>();
        }

        protected Teacher()
        {

        }

        private List<Test> _tests;

        public IReadOnlyCollection<Test> CreatedTests => _tests;

    }
}
