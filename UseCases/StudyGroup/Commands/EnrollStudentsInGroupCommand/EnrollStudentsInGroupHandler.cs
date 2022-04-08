using DataAccess.Interfaces;
using Entities.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.EnrollStudentsInGroupCommand
{
    public class EnrollStudentsInGroupHandler : IRequestHandler<EnrollStudentsInGroupRequest, EnrollStudentsDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<EnrollStudentsInGroupHandler> _logger;

        public EnrollStudentsInGroupHandler(IDbContext dbContext, ILogger<EnrollStudentsInGroupHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<EnrollStudentsDto> Handle(EnrollStudentsInGroupRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var group = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.Id == request.GroupId);

            if (group is null)
                throw new GroupNotFoundException();

            var students = _dbContext.Participants
                .OfType<Entities.Participants.Student>()
                .Include(x => x.StudentStudyGroups)
                .Where(x => request.Students.Any(y => y == x.Id))
                .AsEnumerable();

            foreach (var student in students)
            {
                try
                {
                    student.Enroll(group);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            await _dbContext.SaveChangesAsync();

            return new EnrollStudentsDto(new SimpleStudyGroupDto(group.Id, group.Title, group.CreateDate),
                students.Select(x => new Common.Dto.StudentDto(x.Id, x.Name, x.Surname, x.Login)));
        }
    }
}
