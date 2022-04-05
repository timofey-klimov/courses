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

            var result = await _dbContext.StudyGroups
                .Include(g => g.Students)
                .Select(x => new
                {
                    Id = x.Id,
                    Students = x.Students.Where(x => request.StudentsId.Contains(x.StudentId)).Select(x => x.Student).ToList(),
                    Group = x
                })
                .FirstOrDefaultAsync(x => x.Id == request.StudyGroupId);
            
            if (result is null || result.Group is null)
                throw new GroupNotFoundException();

            result.Group.RemoveStudents(result.Students);

            await _dbContext.SaveChangesAsync();

            return new RemoveStudentsFromStudyGroupDto(result.Id, result.Students.Select(x => x.Id));
        }
    }
}
