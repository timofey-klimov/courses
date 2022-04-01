using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand;
using UseCases.StudyGroup.Commands.CreateStudyGroupCommand;
using UseCases.StudyGroup.Commands.EnrollStudentsInGroupCommand;
using UseCases.StudyGroup.Dto;
using UseCases.StudyGroup.Queries.GetAllStudentGroups;
using Wep.App.Controllers.Base;
using Wep.App.Dto.Request.StudyGroups;
using Wep.App.Dto.Request.Test;
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
        public async Task<ApiResponse<CreateStudyGroupDto>> CreateStudyGroup([FromBody] CreateStudyGroupRequestDto request, 
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new CreateStudyGroupRequest(request.Title, request.Students, request.Teacher), 
                cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("enroll")]
        public async Task<ApiResponse<EnrollStudentsDto>> EnrollStudents([FromBody] EnrollStudentsRequestDto request, 
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new EnrollStudentsInGroupRequest(request.Group, request.Students), 
                cancellationToken));
        }

        [HttpGet("all/student/groups")]
        public async Task<ApiResponse<IEnumerable<StudyGroupDto>>> GetAllStudentGroups(int studentId,int teacherId,CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllStudentGroupsRequest(studentId, teacherId), cancellationToken));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("assignTest")]
        public async Task<ApiResponse<AssignedTestDto>> AssignTestOnStudyGroup([FromBody] AssignTestOnStudyGroupDto request, 
            CancellationToken token)
        {
           return Ok(await Mediator.Send(new AssignTestOnStudyGroupRequest(request.GroupId, request.TestId, request.Deadline), token));
        }
    }
}
