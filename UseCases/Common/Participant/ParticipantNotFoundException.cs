using Entities.Exceptions;

namespace UseCases.Common.Participant
{
    public class ParticipantNotFoundException : ApiException
    {
        public ParticipantNotFoundException() 
            : base(ExceptionCodes.ParticipantNotFound)
        {
        }
    }
}
