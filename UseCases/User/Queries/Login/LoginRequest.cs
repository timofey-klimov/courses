using MediatR;
using UseCases.User.Dto;

namespace UseCases.User.Queries.Login
{
    public class LoginRequest : IRequest<LoginResultDto>
    {
        public string Login { get; }

        public string Password { get; }

        public LoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
