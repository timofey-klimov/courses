using MediatR;
using System.Collections.Generic;
using UseCases.AssignTest.Dto;

namespace UseCases.AssignTest.Queries.GetAssignedOnStudentTestsQuery
{
    public record GetAssignedOnStudentTestsRequest() 
        : IRequest<IEnumerable<GetAssignedTestDto>>;
   
}
