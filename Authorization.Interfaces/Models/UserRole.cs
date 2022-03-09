namespace Authorization.Interfaces.Models
{
    public class UserRole
    {
        public string Role { get; }

        public UserRole(string role)
        {
            Role = role;
        }
    }
}
