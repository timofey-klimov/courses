using DataAccess.Interfaces;
using Entities.Participants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Test.Dto;

namespace UseCases.Test.Queries.GetAllTestQuery
{
    public class GetAllTestQueryHandler : IRequestHandler<GetAllTestQueryRequest, IEnumerable<TestDto>>
    {
        private readonly IDbContext _dbContext;
        public GetAllTestQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<TestDto>> Handle(GetAllTestQueryRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var teacher = await _dbContext.Participants
                .OfType<Teacher>()
                .Include(x=>x.CreatedTests)
                .Select(x => new
                {
                    Id = x.Id,
                    Tests = x.CreatedTests.Select(x => new
                    {
                        x.Id,
                        x.Title,
                        x.CreateDate
                    })
                   
                })
                .FirstOrDefaultAsync(x=>x.Id == request.TeacherId);
            return teacher.Tests.Select(test => new TestDto {Id = test.Id, Title = test.Title, CreateDate = test.CreateDate});
        }
    }
}
