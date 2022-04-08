﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Dto;
using UseCases.Teachers.Queries.GetTeachersGroupsQuery;
using UseCases.Teachers.Queries.GetTeachersQuery;
using UseCases.Teachers.Queries.GetTeachersTestsQuery;
using UseCases.Test.Dto;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.StudyGroups;
using Wep.App.Dto.Request.Teacher;
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
        public async Task<ApiResponse<Pagination<TeacherDto>>> GetTeachers([FromQuery] GetTeachersDto dto, 
            CancellationToken token)
        { 
            return Ok(await Mediator.Send(new GetTeachersRequest(dto.Name, dto.Surname, dto.Offset, dto.Limit), token));
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("tests")]
        public async Task<ApiResponse<Pagination<TestDto>>> GetTeacherTests([FromQuery] GetTestsRequest request, 
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetTeachersTestsQueryRequest(request.Offset, request.Limit), token));
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("groups")]
        public async Task<ApiResponse<Pagination<SimpleStudyGroupDto>>> GetTeacherGroups([FromQuery] GetGroupsRequestDto request, 
            CancellationToken token)
        {
            return Ok(await Mediator.Send(new GetTeachersGroupsQueryRequest(request.Title, request.StartDate, request.EndDate,
                request.Offset, request.Limit), token));
        }
    }
}
