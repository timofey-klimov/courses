using DataAccess.Interfaces;
using Entities.Participants;
using Entities.Participants.States;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;

namespace UseCases.Students.Queries.GetStudentsQuery
{
    public class GetStudentsRequestHandler : IRequestHandler<GetStudentsRequest, Pagination<StudentDto>>
    {
        private readonly IDbContext _dbContext;
        public GetStudentsRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Pagination<StudentDto>> Handle(GetStudentsRequest request, CancellationToken cancellationToken)
        {
            var count = await _dbContext.Participants
                .OfType<Student>()
                .Where(x => x.State != ParticipantState.Blocked)
                .CountAsync();

            var students = _dbContext.Participants
                .OfType<Student>()
                .Where(x => x.State != ParticipantState.Blocked)
                .Skip(request.Offset)
                .Take(request.Limit)
                .AsEnumerable();

            return new Pagination<StudentDto>(students.Select(
                x => new StudentDto(x.Id, x.Name, x.Surname, x.Login)), count);
        }
    }
}
