using MediatR;
using UseCases.Common.User.Model;
using UseCases.User.Dto;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserRequest : IRequest
    {
        public string Name { get; }

        public string Surname { get; }

        public string Login { get; }

        public string Password { get; }

        public string UserRole { get; }

        public CreateUserRequest(string login, string password, string name, string surname, string role)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            UserRole = role;
        }
    }
}
