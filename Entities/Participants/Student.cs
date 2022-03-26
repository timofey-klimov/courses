namespace Entities.Participants
{
    public class Student : Participant
    {
        protected Student() { }
        public Student(string login, string name, string surname, string password, string hashedPassword, ParticipantRole userRole) 
            : base(login, name, surname, password, hashedPassword, userRole)
        {
        }
    }
}
