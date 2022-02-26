using Entities.Exceptions;

namespace UseCases.Common.Exceptions
{
    public class AccessDeniedException : ApiException
    {
        public AccessDeniedException() 
            : base(ExceptionCodes.AccessDenied)
        {
        }
    }
}
