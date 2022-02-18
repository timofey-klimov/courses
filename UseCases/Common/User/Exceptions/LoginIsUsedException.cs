using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class LoginIsUsedException : ExceptionBase
    {
        public LoginIsUsedException() 
            : base(ExceptionCodes.LoginInUsed)
        {
        }
    }
}
