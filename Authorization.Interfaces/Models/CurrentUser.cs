using System;

namespace Authorization.Interfaces.Models
{
    public class CurrentUser
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Login { get; }

        public string Surname { get; }

        public CurrentUser(Guid id, string name, string login, string surname)
        {
            Id = id;
            Name = name;
            Login = login;
            Surname = surname;
        }
    }
}
