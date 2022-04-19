using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Common.Participant;

namespace UseCases.Teachers.Queries.GetStudentsQuery
{
    public class GetTeacherStudentsRequestHandler : IRequestHandler<GetTeacherStudentsRequest, Pagination<StudentDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetTeacherStudentsRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<StudentDto>> Handle(GetTeacherStudentsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.StudentTeachers)
                    .ThenInclude(x => x.Student)
                .Select(x => new
                {
                    Id = x.Id,
                    Students = x.StudentTeachers
                        .Select(x => new
                        {
                            Id = x.StudentId,
                            Name = x.Student.Name,
                            Surname = x.Student.Surname,
                            Login = x.Student.Login
                        })
                        .Skip(request.Offset)
                        .Take(request.Limit),

                    Count = x.StudentTeachers.Count()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (result == null)
                throw new ParticipantNotFoundException();

            return new Pagination<StudentDto>
                (result.Students.Select(x => new StudentDto(x.Id, x.Name, x.Surname, x.Login)), result.Count);
        }

    }
}
