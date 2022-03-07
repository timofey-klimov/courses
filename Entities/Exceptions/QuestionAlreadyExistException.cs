namespace Entities.Exceptions
{
    public class QuestionAlreadyExistException : ApiException
    {
        public QuestionAlreadyExistException() 
            : base(ExceptionCodes.QuestionAlreadyExist)
        {
        }
    }
}
