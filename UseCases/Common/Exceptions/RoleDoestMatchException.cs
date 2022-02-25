using Entities.Exceptions;

namespace UseCases.Common.Exceptions
{
    public class RoleDoestMatchException : ApiException
    {
        public RoleDoestMatchException() 
            : base(ExceptionCodes.RoleDoesntMatch)
        {
        }
    }
}
