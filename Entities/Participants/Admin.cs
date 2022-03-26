namespace Entities.Participants
{
    public class Admin : Participant
    {
        protected Admin() { }

        public Admin(string login, string name, string surname, string password, string hashedPassword, ParticipantRole role) 
            : base(login, name, surname, password, hashedPassword, role)
        {

        }
    }
}
