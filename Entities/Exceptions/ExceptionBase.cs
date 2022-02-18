using System;

namespace Entities.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public ExceptionCodes Code { get; }

        public ExceptionBase(ExceptionCodes code)
        {
            Code = code;
        }
    }
}
