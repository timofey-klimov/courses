namespace Entities.Exceptions
{
    public class GroupAlreadyAssignedException : ApiException
    {
        public GroupAlreadyAssignedException() 
            : base(ExceptionCodes.GroupAllreadyAssigned)
        {
        }
    }
}
