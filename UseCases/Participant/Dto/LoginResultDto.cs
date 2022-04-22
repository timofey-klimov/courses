using UseCases.Common.Dto.Participants;

namespace UseCases.Participant.Dto
{
    public record LoginResultDto(string Token, ParticipantInfoDto Participant);
   
}
