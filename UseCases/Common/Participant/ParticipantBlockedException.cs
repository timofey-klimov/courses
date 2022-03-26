using Entities.Exceptions;

namespace UseCases.Common.Exceptions
{
    public class ParticipantBlockedException : ApiException
    {
        /// <summary>
        /// Код107
        /// </summary>
        public ParticipantBlockedException() 
            : base(ExceptionCodes.ParticipantBlocked)
        {
        }
    }
}
