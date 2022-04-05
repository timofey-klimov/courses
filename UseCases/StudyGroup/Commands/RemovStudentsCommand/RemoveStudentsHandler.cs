using DataAccess.Interfaces;
using Entities.Exceptions;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.StudyGroup.Dto;

namespace UseCases.StudyGroup.Commands.RemovStudentsCommand
{
    public class RemoveStudentsHandler : IRequestHandler<RemoveStudentsRequest, RemoveStudentsFromStudyGroupDto>
    {
        private readonly IDbContext _dbContext;
        public RemoveStudentsHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<RemoveStudentsFromStudyGroupDto> Handle(RemoveStudentsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var group = await _dbContext.StudyGroups
                .Include(g => g.Students)
                .FirstOrDefaultAsync(x => x.Id == request.studyGroupId);
            
            if (group is null)
                throw new GroupNotFoundException();

            var students = await _dbContext.Participants
                .OfType<Student>()
                .Where(x => request.studentsId.Contains(x.Id))
                .ToListAsync();
            
            group.RemoveStudents(students);
            await _dbContext.SaveChangesAsync();

            return new RemoveStudentsFromStudyGroupDto(group.Id, students.Select(x=>x.Id).ToList());
        }
    }
}
