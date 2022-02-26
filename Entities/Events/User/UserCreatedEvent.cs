namespace Entities.Events.User
{
    public class UserCreatedEvent : DomainEvent
    {
        public string Email { get; }

        public string Password { get; }

        public UserCreatedEvent(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
