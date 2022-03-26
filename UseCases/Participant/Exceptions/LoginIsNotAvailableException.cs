using Entities.Exceptions;

namespace UseCases.Participant.Exceptions
{
    public class LoginIsNotAvailableException : ApiException
    {
        public LoginIsNotAvailableException() 
            : base(ExceptionCodes.LoginIsNotAvailable)
        {
        }
    }
}
