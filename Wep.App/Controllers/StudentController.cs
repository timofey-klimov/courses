using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Students.Commands.AssignTestsOnStudentsCommand;
using UseCases.Students.Queries.GetStudentsQuery;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.Test;
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
        public async Task<ApiResponse<Pagination<StudentDto>>> GetStudents([FromQuery] GetStudentsRequest request, 
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetStudentsRequest(request.Offset, request.Limit), cancellationToken));
        }
        [Authorize(Roles = "Teacher")]
        [HttpPut("students/assignTests/{testId}/enroll")]
        public async Task<ApiResponse<AssignTestDto>> AssignTestOnStudents(int testId, [FromBody] IEnumerable<int> studentsId, DateTime deadline,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AssignTestsOnStudentsRequest(testId, studentsId, deadline), cancellationToken));
        }
    }
}
