namespace Entities.Exceptions
{
    public class GroupCantBeEmptyException : ApiException
    {
        public GroupCantBeEmptyException() 
            : base(ExceptionCodes.GroupCantBeEmpty)
        {
        }
    }
}
