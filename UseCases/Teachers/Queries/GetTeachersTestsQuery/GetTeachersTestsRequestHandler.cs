using Authorization.Interfaces;
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
using UseCases.Common.Dto;
using UseCases.Common.Participant;
using UseCases.Test.Dto;

namespace UseCases.Teachers.Queries.GetTeachersTestsQuery
{
    public class GetTeachersTestsRequestHandler : IRequestHandler<GetTeachersTestsQueryRequest, Pagination<TestDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetTeachersTestsRequestHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<Pagination<TestDto>> Handle(GetTeachersTestsQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x => x.CreatedTests)
                .Select(x => new
                {
                    Count = x.CreatedTests.Count(),
                    Tests = x.CreatedTests
                        .Skip(request.Offset)
                        .Take(request.Limit)
                        .Select(x => new
                        {
                            x.Id,
                            x.Title,
                            x.CreateDate
                        }),
                    x.Id
                })
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (result == null)
                throw new ParticipantNotFoundException();

            return new Pagination<TestDto>(result.Tests.Select(x => new TestDto()
            {
                Id = x.Id,
                CreateDate = x.CreateDate,
                Title = x.Title
            }), result.Count);
        }
    }
}
