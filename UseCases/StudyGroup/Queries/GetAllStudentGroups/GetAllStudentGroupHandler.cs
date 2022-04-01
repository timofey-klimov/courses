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

namespace UseCases.StudyGroup.Queries.GetAllStudentGroups
{
    public class GetAllStudentGroupHandler : IRequestHandler<GetAllStudentGroupsRequest, IEnumerable<StudyGroupDto>>
    {
        private readonly IDbContext _dbContext;
        public GetAllStudentGroupHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<StudyGroupDto>> Handle(GetAllStudentGroupsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var teacher = (await _dbContext.Participants
                        .OfType<Teacher>()
                        .Include(x => x.StudyGroups)
                        .FirstOrDefaultAsync(x => x.Id == request.TeacherId));

            if (teacher == null || teacher.StudyGroups == null)
                throw new GroupNotFoundException();

            var result = await _dbContext.Participants
                .OfType<Student>()
                .Include(x => x.StudentStudyGroups)
                    .ThenInclude(x => x.Student)
                .Include(x => x.StudentStudyGroups)
                    .ThenInclude(x => x.StudyGroup)
                .Select(x => new
                {
                    StudentId = x.Id,
                    Groups = x.EnrolledGroups()
                })
                .FirstOrDefaultAsync(x => x.StudentId == request.StudentId);


            return teacher.StudyGroups.Except(result.Groups)
                    .Select(x => new StudyGroupDto(x.Id, x.Title));
        }
    }
}
