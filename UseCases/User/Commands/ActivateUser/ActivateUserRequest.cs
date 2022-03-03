using MediatR;

namespace UseCases.User.Commands.ActivateUser
{
    public class ActivateUserRequest : IRequest
    {
        public string Password { get; }

        public ActivateUserRequest(string password)
        {
            Password = password;
        }
    }
}
