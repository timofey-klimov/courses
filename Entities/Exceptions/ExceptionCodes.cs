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

        GroupAllreadyAssigned = 201,
        StudentAlreadyEnroll = 202,
        GroupAlreadyExist = 203,
        GroupCantBeEmpty = 204,
        GroupNotFound = 205,
        StudentNotFound = 206,

        TestAlreadyExists = 301,
        TestNotFound = 302,
        TestAlreadyAssigned = 303
    }
}
