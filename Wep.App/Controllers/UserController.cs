using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.User.Commands.ActivateUser;
using UseCases.User.Commands.BlockUserCommand;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Dto;
using UseCases.User.Queries.CheckLoginAvailable;
using UseCases.User.Queries.GetUser;
using UseCases.User.Queries.GetUserForPagination;
using UseCases.User.Queries.Login;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/user")]
    public class UserController : ApplicationController
    {
        public UserController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpPost("sign-in")]
        public async Task<ApiResponse<LoginResultDto>> Login([FromBody] LoginUserRequest request, CancellationToken token)
        {
            var result = await Mediator.Send(new LoginRequest(request.Login, request.Password), token);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("sign-up")]
        public async Task<ApiResponse> Register([FromBody] RegisterUserRequest request, CancellationToken token)
        {
            await Mediator.Send(new CreateUserRequest(request.Login, request.Password, request.Name, request.Surname, request.Role), token);
            return Ok();
        }

        [Authorize]
        [HttpPost("activate")]
        public async Task<ApiResponse> ActivateUser([FromBody] ActivateUserDto request, CancellationToken token)
        {
            await Mediator.Send(new ActivateUserRequest(request.Password), token);
            return Ok();
        }


        [Authorize]
        [HttpPost("login-available")]
        public async Task<ApiResponse> CheckIsLoginAvailable([FromBody] CheckLoginAvailableDto request, CancellationToken token)
        {
            await Mediator.Send(new CheckLoginAvailableRequest(request.Login), token);
            return Ok();
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<ApiResponse<UserDto>> GetUser(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserRequest(), cancellationToken));
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("all")]
        public async Task<ApiResponse<Pagination<PaginationUserDto>>> GetUsers([FromQuery] FilterUserRequest filter, CancellationToken token)
        {
            var result = await Mediator.Send(new GetUsersForPaginationRequest(filter.Offset, filter.Limit, filter.Name, filter.Surname, filter.Login), token);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("block/{id}")]
        public async Task<ApiResponse> BlockUser(int id, CancellationToken token)
        {
            await Mediator.Send(new BlockUserRequest(id), token);

            return Ok();
        }
    }
}
