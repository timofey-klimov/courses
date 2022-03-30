namespace Entities.Exceptions
{
    public class TestAlreadyAssignedException : ApiException
    {
        public TestAlreadyAssignedException()
            : base(ExceptionCodes.TestAlreadyAssigned)
        {
        }
    }
}
