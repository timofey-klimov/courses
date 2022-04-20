using Application.Interfaces.StudyGroups;
using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Common.Services.Abstract.Mapper;
using UseCases.StudyGroup.Dto;
using UseCases.Test.Exceptions;

namespace UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand
{
    public class AssignTestOnStudyGroupHandler : IRequestHandler<AssignTestOnStudyGroupRequest, AssignedTestDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<AssignTestOnStudyGroupHandler> _logger;
        private readonly IStudyGroupService _assignedTestManager;
        private readonly IAssignTestMapper _mapper;

        public AssignTestOnStudyGroupHandler(
            IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider,
            ILogger<AssignTestOnStudyGroupHandler> logger,
            IStudyGroupService assignedTestManager,
            IAssignTestMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _assignedTestManager = assignedTestManager ?? throw new ArgumentNullException(nameof(assignedTestManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AssignedTestDto> Handle(AssignTestOnStudyGroupRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Include(x => x.CreatedTests)
                .Select(x => new
                {
                    TeacherId = x.Id,
                    Test = x.GetCreatedTest(request.TestId)
                })
                .FirstOrDefaultAsync(x => x.TeacherId == _currentUserProvider.GetUserId());

            if (result == null || result.Test == null)
                throw new TestNotFoundException();

            var groupInfo = await _dbContext.StudyGroups
                .Include(x => x.Students)
                    .ThenInclude(x => x.Student)
                        .ThenInclude(x => x.StudentAssignedTests)
                            .ThenInclude(x => x.AssignedTest)
                .Include(x => x.Students)
                    .ThenInclude(x => x.StudyGroup)
                .Include(x => x.AssignedTests)
                .Select(x => new
                {
                    GroupId = x.Id,
                    Group = x,
                })
                .FirstOrDefaultAsync(x => x.GroupId == request.GroupId);

            if (groupInfo == null || groupInfo.Group == null)
                throw new GroupNotFoundException();

            var assignTest = new AssignedTest(result.Test, request.Deadline);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _assignedTestManager.AssignTestOnStudyGroup(assignTest, 
                        groupInfo.Group, (x) => _logger.LogError(x));

                    groupInfo.Group.AssignTest(assignTest);

                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message, ex);
                }
            }

            return _mapper.ToAssignTestDto(assignTest);
        }
    }
}
