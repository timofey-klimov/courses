using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.CreateTest;
using UseCases.Test.Dto;
using UseCases.Test.Queries.GetAllTestQuery;
using UseCases.Test.Queries.GetTestQuery;
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
        
        [Authorize(Roles = "Teacher")]
        [HttpGet("all")]
        public async Task<ApiResponse<IEnumerable<TestDto>>> GetAllTests(CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetAllTestQueryRequest(), token));
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("info/{id}")]
        public async Task<ApiResponse<TestWithQuestionsDto>> GetTest(int id)
        {
            return Ok(await Mediator.Send(new GetTestRequest(id)));
        }
    }
}
