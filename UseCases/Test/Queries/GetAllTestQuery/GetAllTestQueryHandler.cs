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
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetAllTestQuery
{
    public class GetAllTestQueryHandler : IRequestHandler<GetAllTestQueryRequest, IEnumerable<TestDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetAllTestQueryHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TestDto>> Handle(GetAllTestQueryRequest request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x=>x.CreatedTests)
                .FirstOrDefaultAsync(x=>x.Id == _currentUserProvider.GetUserId());
            return teacher.CreatedTests.Select(test => new TestDto {Id = test.Id, Title = test.Title, CreateDate = test.CreateDate});
        }
    }
}
