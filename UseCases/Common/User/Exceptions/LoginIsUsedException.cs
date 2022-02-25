using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class LoginIsUsedException : ApiException
    {
        public LoginIsUsedException() 
            : base(ExceptionCodes.LoginInUsed)
        {
        }
    }
}
