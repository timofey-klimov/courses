using MediatR;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Queries.GetParticipantInfoQuery
{
    public record GetParticipantInfoRequest : IRequest<ParticipantDto>;
}
