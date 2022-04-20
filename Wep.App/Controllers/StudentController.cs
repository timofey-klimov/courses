using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Students.Commands.AssignTestsOnStudentsCommand;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.Students;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/students")]
    public class StudentController : ApplicationController
    {
        public StudentController(IMediator mediator)
            : base(mediator)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ApiResponse<Pagination<StudentDto>>> GetStudents([FromQuery] Wep.App.Dto.Request.Students.GetStudentsRequest request, 
            CancellationToken cancellationToken)
        {
            return Ok(
                await Mediator.Send(new UseCases.Students.Queries.GetStudentsQuery.GetStudentsRequest(request.Offset, request.Limit), cancellationToken));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("students/assignTests/{testId}/enroll")]
        public async Task<ApiResponse<AssignedTestDto>> AssignTestOnStudents(int testId, [FromBody] AssignTestRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AssignTestsOnStudentsRequest(testId, request.Students, request.Deadline), cancellationToken));
        }
    }
}
