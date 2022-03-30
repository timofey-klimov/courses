using Entities.Exceptions;

namespace UseCases.Test.Exceptions
{
    public class TestNotFoundException : ApiException
    {
        public TestNotFoundException() 
            : base(ExceptionCodes.TestNotFound)
        {
        }
    }
}
