using MediatR;
using System;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;

namespace UseCases.Teachers.Queries.GetTeachersGroupsQuery
{
    public record GetTeachersGroupsQueryRequest(string Title, DateTime? startDate, DateTime? endDate, 
        int offset, int limit) : IRequest<Pagination<SimpleStudyGroupDto>>;
}
