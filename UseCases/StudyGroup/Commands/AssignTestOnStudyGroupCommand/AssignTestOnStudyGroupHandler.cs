using DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.StudyGroup.Commands.AssignTestOnStudyGroupCommand
{
    public class AssignTestOnStudyGroupHandler : IRequestHandler<AssignTestOnStudyGroupRequest>
    {
        private readonly IDbContext _dbContext;
        public AssignTestOnStudyGroupHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Unit> Handle(AssignTestOnStudyGroupRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
