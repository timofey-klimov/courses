using Entities.Exceptions;

namespace UseCases.Participant.Exceptions
{
    public class ParticipantAlreadyExistException : ApiException
    {
        public ParticipantAlreadyExistException() 
            : base(ExceptionCodes.ParticipantExisted)
        {
        }
    }
}
