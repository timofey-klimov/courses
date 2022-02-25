using MediatR;
using UseCases.Common.User.Model;
using UseCases.User.Dto;

namespace UseCases.User.Queries.Login
{
    public class LoginRequest : IRequest<AuthUserDto>
    {
        public string Login { get; }

        public string Password { get; }

        public UserRole Role { get; }

        public LoginRequest(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
