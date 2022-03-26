using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.CreateTest;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.Test;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers
{
    [Route("api/test")]
    public class TestController : ApplicationController
    {
        public TestController(IMediator mediator) 
            : base(mediator)
        {
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost("create")]
        public async Task<ApiResponse> CreateTest([FromBody] CreateTestDto dto, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateTestRequest(dto.Questions, dto.Title), token);

            return Ok(result);
        }
    }
}
