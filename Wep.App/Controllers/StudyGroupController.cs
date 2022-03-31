using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Commands.CreateStudyGroupCommand;
using UseCases.StudyGroup.Dto;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.StudyGroups;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/studyGroups")]
    public class StudyGroupController : ApplicationController
    {
        public StudyGroupController(IMediator mediator) 
            : base(mediator)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<ApiResponse<CreateStudyGroupDto>> CreateStudyGroup([FromBody] CreateStudyGroupRequestDto dto, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new CreateStudyGroupRequest(dto.Title, dto.Students, dto.Teacher), cancellationToken));
        }
    }
}
