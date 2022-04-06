using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/tests")]
    public class TestController : ApplicationController
    {
        private readonly IMapper _mapper;
        public TestController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            _mapper = mapper;
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("create")]
        public async Task<ApiResponse<TestDto>> CreateTest([FromBody] CreateTestDto dto, 
            CancellationToken token)
        {
            var request = _mapper.Map<CreateTestRequest>(dto);

            return Ok(await Mediator.Send(request, token));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("{teacherId}/all")]
        public async Task<ApiResponse<IEnumerable<TestDto>>> GetAllTests(int teacherId, 
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetAllTestQueryRequest(teacherId), token));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{teacherId}/{testId}/info")]
        public async Task<ApiResponse<TestWithQuestionsDto>> GetTestInfo(int teacherId, int testId)
        {
            return Ok(await Mediator.Send(new GetTestQueryRequest(teacherId, testId)));
        }
    }
}
