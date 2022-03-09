namespace Entities.Users
{
    public class User : Participant
    {
        protected User() { }
        public User(string login, string name, string surname, string password, string hashedPassword, UserRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
        }
    }
}
