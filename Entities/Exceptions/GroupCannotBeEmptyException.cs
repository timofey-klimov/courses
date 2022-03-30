namespace Entities.Exceptions
{
    public class GroupCannotBeEmptyException : ApiException
    {
        public GroupCannotBeEmptyException()
            : base(ExceptionCodes.GroupCannotBeEmpty)
        {
        }
    }
}
