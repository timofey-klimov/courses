namespace Entities.Exceptions
{
    public class UserAlreadyActiveException : ApiException
    {
        public UserAlreadyActiveException() 
            : base(ExceptionCodes.UserAlreadyActive)
        {
        }
    }
}
