using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException() 
            : base(ExceptionCodes.UserNotFound)
        {
        }
    }
}
