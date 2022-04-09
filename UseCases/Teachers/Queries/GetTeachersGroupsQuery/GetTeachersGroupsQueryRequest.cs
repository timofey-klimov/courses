using MediatR;
using System;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;
using UseCases.Teachers.Dto;

namespace UseCases.Teachers.Queries.GetTeachersGroupsQuery
{
    public record GetTeachersGroupsQueryRequest(string Title, DateTime? startDate, DateTime? endDate, 
        int offset, int limit) : IRequest<Pagination<StudyGroupWithStudentCount>>;
}
