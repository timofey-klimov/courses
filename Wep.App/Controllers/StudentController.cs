using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Students.Queries.GetStudentsQuery;
using Wep.App.Controllers.Base;
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
        public async Task<ApiResponse<IEnumerable<StudentDto>>> GetStudents(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetStudentsRequest(), cancellationToken));
        }
    }
}
