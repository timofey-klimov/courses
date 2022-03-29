namespace Wep.App.Dto.Request.Participant
{
    public class CreateParticipantDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
