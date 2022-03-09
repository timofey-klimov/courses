using System;
using System.Collections.Generic;

namespace Authorization.Interfaces.Models
{
    public class CurrentUser
    {
        public string Name { get; }

        public string Login { get; }

        public string Surname { get; }

        public IEnumerable<UserRole> Roles { get; }

        public CurrentUser(string name, string login, string surname, IEnumerable<UserRole> roles)
        {
            Name = name;
            Login = login;
            Surname = surname;
            Roles = roles;
        }
    }
}
