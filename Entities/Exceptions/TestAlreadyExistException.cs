namespace Entities.Exceptions
{
    public class TestAlreadyExistException : ApiException
    {
        /// <summary>
        /// Code 301
        /// </summary>
        public TestAlreadyExistException() 
            : base(ExceptionCodes.TestAlreadyExists)
        {
        }
    }
}
