namespace Entities.Exceptions
{
    public enum ExceptionCodes : int
    {
        ParticipantExisted = 100,
        LoginIsNotAvailable = 101,
        ParticipantNotFound = 102,
        AccessDenied = 103,
        PasswordMatches = 104,
        QuestionAlreadyExist = 105,
        ParticipantAlreadyActive = 106,
        ParticipantBlocked = 107,

        GroupCannotBeEmpty = 201,
        GroupAllreadyAssigned = 202,
        StudentAlreadyEnroll = 203,

        TestAlreadyExists = 301,
        TestNotFound = 302,
        TestAlreadyAssigned = 303
    }
}
