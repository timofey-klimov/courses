using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.Teacher.Contains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;

namespace UseCases.Teachers.Queries.GetTeachersQuery
{
    public class GetTeachersRequestHandler : IRequestHandler<GetTeachersRequest, IEnumerable<TeacherDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IFilterProvider _filterProvider;
        public GetTeachersRequestHandler(IDbContext dbContext, IFilterProvider filterProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _filterProvider = filterProvider ?? throw new ArgumentNullException(nameof(filterProvider));
        }

        public async Task<IEnumerable<TeacherDto>> Handle(GetTeachersRequest request, CancellationToken cancellationToken)
        {
            var spec = new TeacherFilterSpecification(request.Name, request.Surname);

            var query = _dbContext.Participants
                .OfType<Entities.Participants.Teacher>();

            var teachers = _filterProvider.GetQuery(query, spec)
                .AsEnumerable();

            return teachers.Select(x => new TeacherDto(x.Id, x.Name, x.Surname, x.Login));
        }
    }
}
