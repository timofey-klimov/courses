namespace Entities.Exceptions
{
    public class GroupNotFoundException : ApiException
    {
        public GroupNotFoundException() 
            : base(ExceptionCodes.GroupNotFound)
        {
        }
    }
}
