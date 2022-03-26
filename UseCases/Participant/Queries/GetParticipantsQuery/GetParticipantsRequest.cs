using MediatR;
using UseCases.Common.Dto;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Queries.GetParticipantsQuery
{
    public record GetParticipantsRequest(string Name, string Surname, string Login, bool IsOnlyActive, int Offset, int Limit)
        : IRequest<Pagination<ParticipantDto>>;
    
}
