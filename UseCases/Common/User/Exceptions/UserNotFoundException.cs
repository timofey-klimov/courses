using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class UserNotFoundException : ExceptionBase
    {
        public UserNotFoundException() 
            : base(ExceptionCodes.UserNotFound)
        {
        }
    }
}
