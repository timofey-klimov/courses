namespace Wep.App.Dto.Request.Participant
{
    public class FilterParticipantDto
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;
        public string Login { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public bool IsOnlyActive { get; set; } = false;
    }
}
