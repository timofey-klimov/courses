using DataAccess.Interfaces;
using Entities.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.StudyGroup.Commands.DeleteStudyGroupCommand
{
    public class DeleteStudyGroupHandler : IRequestHandler<DeleteStudyGroupRequest, int>
    {
        private readonly IDbContext _dbContext;
        public DeleteStudyGroupHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<int> Handle(DeleteStudyGroupRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            
            var group = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.Id == request.groupId);
            if (group is null)
                throw new GroupNotFoundException();

            _dbContext.StudyGroups.Remove(group);
            await _dbContext.SaveChangesAsync();
            
            return group.Id;
        }
    }
}
