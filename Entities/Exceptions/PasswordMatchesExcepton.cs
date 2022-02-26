namespace Entities.Exceptions
{
    public class PasswordMatchesExcepton : ApiException
    {
        public PasswordMatchesExcepton()
            : base(ExceptionCodes.PasswordMatches)
        {

        }
    }
}
