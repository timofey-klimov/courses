using DataAccess.Interfaces;
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

            var teacher = _dbContext.Participants
                        .OfType<Teacher>()
                        .Include(x => x.StudyGroups);

            var result = await _dbContext.Participants
                .OfType<Student>()
                .Include(x => x.StudentStudyGroups)
                    .ThenInclude(x => x.Student)
                .Include(x => x.StudentStudyGroups)
                    .ThenInclude(x => x.StudyGroup)
                .Select(x => new
                {
                    StudentId = x.Id,
                    Groups = from t in teacher.FirstOrDefault(x => x.Id == request.TeacherId).StudyGroups
                             join g in x.EnrolledGroups()
                             on t.Id equals g.Id into ps
                             from p in ps.DefaultIfEmpty()
                             where p == null
                             select t

                })
                .FirstOrDefaultAsync(x => x.StudentId == request.StudentId);
            

            //_dbContext.StudyGroups.Where(x => groupsId.Se)



            return default;
        }
    }
}
