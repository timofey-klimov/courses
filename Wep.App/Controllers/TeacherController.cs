using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Teachers.Queries.GetTeachersQuery;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.StudyGroups;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/teachers")]
    public class TeacherController : ApplicationController
    {
        public TeacherController(IMediator mediator) 
            : base(mediator)
        {

        }


        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ApiResponse<IEnumerable<TeacherDto>>> GetTeachers([FromQuery] GetTeachersDto dto, 
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetTeachersRequest(dto.Name, dto.Surname), token));
        }
    }
}
