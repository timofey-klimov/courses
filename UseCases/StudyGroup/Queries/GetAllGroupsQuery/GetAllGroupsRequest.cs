using MediatR;
using System.Collections.Generic;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetAllGroupsQuery
{
    public record GetAllGroupsRequest(int Offset, int Limit) : IRequest<Pagination<StudyGroupDto>>;
    
}
