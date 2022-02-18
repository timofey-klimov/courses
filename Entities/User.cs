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


        public User(string name, string surname, string login, string hashedPassword)
        {
            Login = login;
            Surname = surname;
            Name = name;
            HashedPassword = hashedPassword;
        }
    }
}
