using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class UserNotFoundException : ApiException
    {
        /// <summary>
        /// Код102
        /// </summary>
        public UserNotFoundException() 
            : base(ExceptionCodes.UserNotFound)
        {
        }
    }
}
