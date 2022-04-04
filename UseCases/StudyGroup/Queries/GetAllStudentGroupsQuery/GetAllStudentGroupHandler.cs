using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;
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

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudyGroups)
                    .ThenInclude(x => x.Students)
                .Select(x => new
                {
                    Id = x.Id,
                    Teacher = x,
                    Groups = x.StudyGroups
                        .Where(x => !x.Students.Any(x => x.StudentId == request.StudentId))
                })
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId);

            if (result == null)
                throw new ParticipantNotFoundException();

            return result.Groups
                .Select(x => new StudyGroupDto(x.Id, x.Title, 
                new Common.Dto.TeacherDto(result.Teacher.Id, result.Teacher.Name, result.Teacher.Surname, result.Teacher.Login), x.CreateDate));
        }
    }
}
