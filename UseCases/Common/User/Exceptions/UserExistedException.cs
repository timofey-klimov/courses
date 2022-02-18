using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class UserExistedException : ExceptionBase
    {
        public UserExistedException() 
            : base(ExceptionCodes.UserExisted)
        {
        }
    }
}
