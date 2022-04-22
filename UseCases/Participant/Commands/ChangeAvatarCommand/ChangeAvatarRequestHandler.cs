using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;

namespace UseCases.Participant.Commands.ChangeAvatarCommand
{
    public class ChangeAvatarRequestHandler : IRequestHandler<ChangedAvatarRequest, byte[]>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;

        public ChangeAvatarRequestHandler(
            IDbContext dbContext,
            ICurrentUserProvider currentUserProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }

        public async Task<byte[]> Handle(ChangedAvatarRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Participants
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == _currentUserProvider.GetUserId());

            if (user == null)
                throw new ParticipantNotFoundException();

            using var memoryStream = new MemoryStream();

            request.FileStream.CopyTo(memoryStream);
            var byteArray = memoryStream.ToArray();
            user.ChangeAvatar(byteArray);
            await _dbContext.SaveChangesAsync();

            return byteArray;
        }
    }
}
