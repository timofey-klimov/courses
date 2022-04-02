using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.AssignTest.Dto;
using UseCases.AssignTest.Queries.GetAssignedOnStudentTestsQuery;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/assignTests")]
    public class AssignTestController : ApplicationController
    {
        public AssignTestController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Student")]
        [HttpGet("student/all")]
        public async Task<ApiResponse<IEnumerable<GetAssignedTestDto>>> GetAssigedTests(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetAssignedOnStudentTestsRequest(), token));
        }
    }
}
