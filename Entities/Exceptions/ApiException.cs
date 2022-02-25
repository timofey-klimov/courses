using System;

namespace Entities.Exceptions
{
    public abstract class ApiException : Exception
    {
        public ExceptionCodes Code { get; }

        public ApiException(ExceptionCodes code)
        {
            Code = code;
        }
    }
}
