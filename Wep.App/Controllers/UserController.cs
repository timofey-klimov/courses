using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Commands.CreateUser;
using UseCases.User.Dto;
using UseCases.User.Queries.GetAllUsers;
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
            var result = await Mediator.Send(new LoginRequest(request.Login, request.Password), token);
            return Ok(result);
        }

        [HttpPost("sign-up")]
        public async Task<ApiResponse<AuthUserDto>> Register([FromBody] RegisterUserRequest request, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateUserRequest(request.Login, request.Password, request.Name, request.Surname), token);
            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersRequest());
            return Ok(result);
        }
    }
}
