using Application.Interfaces.AssignTest;
using Entities;
using System;
using UseCases.Common.Dto;
using UseCases.Common.Services.Abstract.Mapper;

namespace UseCases.Common.Services.Implementation.Mapper
{
    public class AssigntestMapper : IAssignTestMapper
    {
        private readonly IAssignTestService _assignTestService;
        public AssigntestMapper(IAssignTestService assignTestService)
        {
            _assignTestService = assignTestService ?? throw new ArgumentNullException(nameof(assignTestService));
        }

        public AssignedTestDto ToAssignTestDto(AssignedTest assignedTest)
        {
            return new AssignedTestDto(
                assignedTest.Id,
                assignedTest.Test.Title,
                assignedTest.CreateDate,
                assignedTest.Deadline,
                _assignTestService.ChecckIfAssignedTestOverdueSoon(assignedTest),
                _assignTestService.CheckIfAssignedTestOverdue(assignedTest));
        }
    }
}
