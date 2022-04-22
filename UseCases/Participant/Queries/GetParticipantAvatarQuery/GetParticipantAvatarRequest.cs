using MediatR;

namespace UseCases.Participant.Queries.GetParticipantAvatarQuery
{
    public record GetParticipantAvatarRequest 
        : IRequest<byte[]>;
}
