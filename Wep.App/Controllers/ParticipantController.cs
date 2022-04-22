using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Participant.Commands.ActivateParticipantCommand;
using UseCases.Participant.Commands.BlockParticipantCommand;
using UseCases.Participant.Commands.ChangeAvatarCommand;
using UseCases.Participant.Commands.CreateParticipantCommand;
using UseCases.Participant.Commands.LoginParticipantCommand;
using UseCases.Participant.Commands.UnblockParticipantCommand;
using UseCases.Participant.Dto;
using UseCases.Participant.Queries.GetParticipantAvatarQuery;
using UseCases.Participant.Queries.GetParticipantInfoQuery;
using UseCases.Participant.Queries.GetParticipantsQuery;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.Participant;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/participants")]
    public class ParticipantController : ApplicationController
    {
        public ParticipantController(IMediator mediator) 
            : base(mediator)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("sign-up")]
        public async Task<ApiResponse<ParticipantDto>> Create([FromBody] CreateParticipantDto request, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateParticipantRequest(request.Login, request.Password, request.Name, request.Surname, request.Role), token);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        public async Task<ApiResponse<LoginResultDto>> Login([FromBody] LoginRequestDto dto, CancellationToken token)
        {
            return Ok(await Mediator.Send(new LoginParticipantRequest(dto.Login, dto.Password)));
        }


        [Authorize]
        [HttpPost("activate")]
        public async Task<ApiResponse> Activate([FromBody] ActivateParticipantDto request, CancellationToken token)
        {
            await Mediator.Send(new ActivateParticipantRequest(request.Password), token);

            return Ok();
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<ApiResponse<ParticipantDto>> GetInfo(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetParticipantInfoRequest(), token));
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("all")]
        public async Task<ApiResponse<Pagination<ParticipantDto>>> GetAll([FromQuery] FilterParticipantDto request, CancellationToken token)
        {
            return Ok(await Mediator.Send(
                new GetParticipantsRequest(request.Name, request.Surname, request.Login, request.IsOnlyActive, request.Offset, request.Limit), token));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("block/{id}")]
        public async Task<ApiResponse<int>> Block(int id, CancellationToken token)
        {
            return Ok(await Mediator.Send(new BlockParticipantRequest(id), token));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("unblock/{id}")]
        public async Task<ApiResponse<int>> Unblock(int id, CancellationToken token)
        {
            return Ok(await Mediator.Send(new UnblockParticipantRequest(id), token));
        }

        [Authorize]
        [HttpPut("avatar/change")]
        public async Task<FileContentResult> CreateAvatar(IFormFile formFile,
            CancellationToken token)
        {
            var result = await Mediator.Send(new ChangedAvatarRequest(formFile.OpenReadStream()), token);

            return new FileContentResult(result, "application/octet-stream");
        }


        [Authorize]
        [HttpGet("avatar")]
        public async Task<FileContentResult> GetAvatar(CancellationToken token)
        {
            var result = await Mediator.Send(new GetParticipantAvatarRequest(), token);

            return new FileContentResult(result, "application/octet-stream");
        }
    }
}
