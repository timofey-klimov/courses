namespace Entities.Exceptions
{
    public class StudentAlreadyEnrollException : ApiException
    {
        public StudentAlreadyEnrollException() 
            : base(ExceptionCodes.StudentAlreadyEnroll)
        {
        }
    }
}
