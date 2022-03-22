namespace Authorization.Interfaces
{
    public interface ICurrentUserProvider
    {
        bool IsAuth();

        bool IsAdmin();

        bool IsManager();

        bool IsUser();

        int GetUserId();
    }
}
