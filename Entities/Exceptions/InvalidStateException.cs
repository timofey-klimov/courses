namespace Entities.Exceptions
{
    public class InvalidStateException : ApiException
    {
        public InvalidStateException() 
            : base(ExceptionCodes.InvalidState)
        {
        }
    }
}
