using Entities.Base;
using System;

namespace Entities
{
    public class User : TrackableEntity<Guid>
    {
        public string Login { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole Role { get; private set; }

        private User()
        {

        }

        public User(string name, string surname, string login, string hashedPassword, UserRole userRole)
        {
            Login = login;
            Surname = surname;
            Name = name;
            HashedPassword = hashedPassword;
            Role = userRole;
        }
    }
}
