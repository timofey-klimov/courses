using MediatR;

namespace UseCases.Participant.Commands.ActivateParticipantCommand
{
    public record ActivateParticipantRequest(string Password) : IRequest;
    
}
