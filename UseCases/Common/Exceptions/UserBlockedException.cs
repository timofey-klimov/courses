using Entities.Exceptions;

namespace UseCases.Common.Exceptions
{
    public class UserBlockedException : ApiException
    {
        /// <summary>
        /// Код107
        /// </summary>
        public UserBlockedException() 
            : base(ExceptionCodes.UserBlocked)
        {
        }
    }
}
