using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Model;
using UseCases.User.Commands.ActivateUser;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Dto;
using UseCases.User.Queries.CheckLoginAvailable;
using UseCases.User.Queries.GetAllUsers;
using UseCases.User.Queries.GetUserRole;
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
        public async Task<ApiResponse<AuthUserDto>> Login([FromBody] LoginUserRequest request, CancellationToken token)
        {
            var result = await Mediator.Send(new LoginRequest(request.Login, request.Password, request.Role), token);
            return Ok(result);
        }

        [Authorize]
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
        [HttpGet("get-all")]
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersRequest());
            return Ok(result);
        }

        [Authorize]
        [HttpPost("login-available")]
        public async Task<ApiResponse> CheckIsLoginAvailable([FromBody] CheckLoginAvailableDto request, CancellationToken token)
        {
            await Mediator.Send(new CheckLoginAvailableRequest(request.Login), token);
            return Ok();
        }

        [Authorize]
        [HttpGet("user-role")]
        public async Task<ApiResponse<UserRole>> GetUserRole()
        {
            var result = await Mediator.Send(new GetUserRoleRequest());

            return Ok(result);
        }
    }
}
