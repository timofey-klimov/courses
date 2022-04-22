namespace UseCases.Common.Dto.Participants
{
    public record ParticipantInfoDto(string Login, string Name, string Surname, string State, string Role, byte[] Avatar);
    
}
