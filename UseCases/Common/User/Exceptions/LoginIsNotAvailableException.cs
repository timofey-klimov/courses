using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class LoginIsNotAvailableException : ApiException
    {
        public LoginIsNotAvailableException() 
            : base(ExceptionCodes.LoginIsNotAvailable)
        {
        }
    }
}
