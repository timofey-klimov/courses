using MediatR;
using System.Collections.Generic;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Queries.GetAllGroupsQuery
{
    public record GetAllGroupsRequest : IRequest<IEnumerable<GetAllStudyGroupsDto>>;
    
}
