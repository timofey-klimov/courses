using DataAccess.Interfaces;
using Entities.Participants;
using Entities.Participants.States;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;

namespace UseCases.Students.Queries.GetStudentsQuery
{
    public class GetStudentsRequestHandler : IRequestHandler<GetStudentsRequest, IEnumerable<StudentDto>>
    {
        private readonly IDbContext _dbContext;
        public GetStudentsRequestHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetStudentsRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.Participants
                .OfType<Student>()
                .Where(x => x.State != ParticipantState.Blocked)
                .AsEnumerable()
                .Select(x => new StudentDto(x.Id, x.Name, x.Surname, x.Login));
        }
    }
}
