using MediatR;

namespace UseCases.User.Queries.CheckLoginAvailable
{
    public class CheckLoginAvailableRequest : IRequest
    {
        public string Login { get; }

        public CheckLoginAvailableRequest(string login)
        {
            Login = login;
        }
    }
}
