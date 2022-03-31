using Entities.Exceptions;

namespace UseCases.StudyGroup.Exceptions
{
    public class GroupAlreadyExistException : ApiException
    {
        public GroupAlreadyExistException() 
            : base(ExceptionCodes.GroupAlreadyExist)
        {
        }
    }
}
