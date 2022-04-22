using MediatR;
using System.IO;

namespace UseCases.Participant.Commands.ChangeAvatarCommand
{
    public record ChangedAvatarRequest(Stream FileStream) 
        : IRequest<byte[]>;
}
