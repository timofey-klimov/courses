using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.Teacher.Contains;
using Entities.Participants.States;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Queries.GetTeachersQuery
{
    public class GetTeachersRequestHandler : IRequestHandler<GetTeachersRequest, Pagination<TeacherDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IFilterProvider _filterProvider;
        public GetTeachersRequestHandler(IDbContext dbContext, IFilterProvider filterProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _filterProvider = filterProvider ?? throw new ArgumentNullException(nameof(filterProvider));
        }

        public async Task<Pagination<TeacherDto>> Handle(GetTeachersRequest request, CancellationToken cancellationToken)
        {
            var spec = new TeacherFilterSpecification(request.Name, request.Surname);

            var query = _dbContext.Participants
                .OfType<Entities.Participants.Teacher>()
                .Where(x => x.State != ParticipantState.Blocked);

            var count = await _filterProvider.GetCountQuery(query, spec);

            var teachers = _filterProvider.GetQuery(query, spec)
                .Skip(request.Offset)
                .Take(request.Limit)
                .AsEnumerable();

            return new Pagination<TeacherDto>(teachers.Select(
                x => new TeacherDto(x.Id, x.Name, x.Surname, x.Login)), count);
        }
    }
}
