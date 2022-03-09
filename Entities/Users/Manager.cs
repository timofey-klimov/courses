using System.Collections.Generic;

namespace Entities.Users
{
    public class Manager : Participant
    {
        public Manager(string login, string name, string surname, string password, string hashedPassword, UserRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
            _tests = new List<Test>();
        }

        protected Manager()
        {

        }

        private List<Test> _tests;

        public IReadOnlyCollection<Test> CreatedTests => _tests;

    }
}
