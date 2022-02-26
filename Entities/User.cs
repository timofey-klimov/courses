using Entities.Base;
using Entities.Events.User;
using Entities.Exceptions;
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

        public bool IsFirstSignIn { get; private set; }

        private User()
        {

        }

        public User(string name, string surname, string login, string password, string hashedPassword, UserRole userRole)
        {
            Login = login;
            Surname = surname;
            Name = name;
            HashedPassword = hashedPassword;
            Role = userRole;
            IsFirstSignIn = true;
            Events.Add(new UserCreatedEvent(login, password));
        }

        public User ChangePassword(string hashedPassword)
        {
            if (HashedPassword == hashedPassword)
                throw new PasswordMatchesExcepton();
             
            HashedPassword = hashedPassword;
            UpdateEntity();

            return this;
        }
    }
}
