using Entities.Exceptions;

namespace UseCases.Common.User.Exceptions
{
    public class UserExistedException : ApiException
    {
        public UserExistedException() 
            : base(ExceptionCodes.UserExisted)
        {
        }
    }
}
