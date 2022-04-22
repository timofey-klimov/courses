using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand;
using UseCases.StudyGroup.Commands.CreateStudyGroupCommand;
using UseCases.StudyGroup.Commands.DeleteStudyGroupCommand;
using UseCases.StudyGroup.Commands.EnrollStudentsInGroupCommand;
using UseCases.StudyGroup.Commands.RemovStudentsCommand;
using UseCases.StudyGroup.Dto;
using UseCases.StudyGroup.Queries.GetAllGroupsQuery;
using UseCases.StudyGroup.Queries.GetAllStudentGroups;
using UseCases.StudyGroup.Queries.GetStudyGroupInfoQuery;
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
        public async Task<ApiResponse<StudyGroupDto>> CreateStudyGroup([FromBody] CreateStudyGroupRequestDto request, 
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

        [Authorize(Roles = "Admin")]
        [HttpGet("vacant")]
        public async Task<ApiResponse<IEnumerable<StudyGroupDto>>> GetAllStudentGroups(int studentId, int teacherId,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllStudentGroupsRequest(studentId, teacherId), cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ApiResponse<Pagination<StudyGroupDto>>> GetAllGroups([FromQuery] FilterStudyGroupsRequest request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllGroupsRequest(request.Offset, request.Limit), cancellationToken));
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost("assignTest")]
        public async Task<ApiResponse<AssignedTestDto>> AssignTestOnStudyGroup([FromBody] AssignTestOnStudyGroupDto request, 
            CancellationToken token)
        {
           return Ok(await Mediator.Send(new AssignTestOnStudyGroupRequest(request.GroupId, request.TestId, request.Deadline), token));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{groupId}")]
        public async Task<ApiResponse<int>> DeleteStudyGroup(int groupId,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteStudyGroupRequest(groupId), cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("removeStudents")]
        public async Task<ApiResponse<RemoveStudentsFromStudyGroupDto>> RemoveStudentsFromStudyGroup([FromBody] RemoveStudentsFromStudyGroupDto dto, 
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new RemoveStudentsRequest(dto.studyGroupId, dto.studentsId), cancellationToken));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("info/{teacherId}/{groupId}")]
        public async Task<ApiResponse<StudyGroupInfoDto>> GetStudyGroupInfo(int teacherId, int groupId, 
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetStudyGroupInfoRequest(groupId, teacherId), cancellationToken));
        }
    }
}
