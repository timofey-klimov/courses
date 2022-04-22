using MediatR;
using UseCases.Common.Dto.Participants;

namespace UseCases.Participant.Queries.GetParticipantInfoQuery
{
    public record GetParticipantInfoRequest : IRequest<ParticipantInfoDto>;
}
