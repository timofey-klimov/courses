using MediatR;

namespace UseCases.Participant.Commands.UnblockParticipantCommand
{
    public record UnblockParticipantRequest(int Id) : IRequest<int>;
}
