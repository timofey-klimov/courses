using System.Collections.Generic;

namespace UseCases.User.Dto
{
    public class UserDto
    {
        private UserDto() { }
        public UserDto(string name, string surname, string login, IEnumerable<string> roles, bool isFirstSign)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Roles = roles;
            IsFirstSign = isFirstSign;
        }

        public string Name { get; }

        public string Surname { get; }

        public string Login { get; }

        public IEnumerable<string> Roles { get; }

        public bool IsFirstSign { get; }
    }
}
