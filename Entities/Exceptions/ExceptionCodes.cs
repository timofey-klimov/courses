namespace Entities.Exceptions
{
    public enum ExceptionCodes : int
    {
        UserExisted = 100,
        LoginIsNotAvailable = 101,
        UserNotFound = 102,
        AccessDenied = 103,
        PasswordMatches = 104,
        QuestionAlreadyExist = 105
    }
}
