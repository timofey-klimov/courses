using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Participant.Commands.BlockParticipantCommand
{
    public record BlockParticipantRequest(int Id) : IRequest<int>;
    
}
