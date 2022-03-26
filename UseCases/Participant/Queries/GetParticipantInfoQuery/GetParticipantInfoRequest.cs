using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Queries.GetParticipantInfoQuery
{
    public record GetParticipantInfoRequest : IRequest<ParticipantDto>;
}
