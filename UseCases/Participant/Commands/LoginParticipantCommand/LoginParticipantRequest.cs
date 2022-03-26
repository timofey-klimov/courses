using MediatR;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Commands.LoginParticipantCommand
{
    public record LoginParticipantRequest(string Login, string Password) : IRequest<LoginResultDto>;
}
 