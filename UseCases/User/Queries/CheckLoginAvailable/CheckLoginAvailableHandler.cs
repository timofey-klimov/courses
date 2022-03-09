using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Common.User.Exceptions;

namespace UseCases.User.Queries.CheckLoginAvailable
{
    public class CheckLoginAvailableHandler : IRequestHandler<CheckLoginAvailableRequest>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IDbContext _dbContext;
        public CheckLoginAvailableHandler(ICurrentUserProvider currentUserProvider, IDbContext dbContext)
        {
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Unit> Handle(CheckLoginAvailableRequest request, CancellationToken cancellationToken)
        {
            if (!_currentUserProvider.IsAdmin())
                throw new AccessDeniedException();

            if (await _dbContext.Participants.AnyAsync(x => x.Login == request.Login))
                throw new LoginIsNotAvailableException();

            return Unit.Value;
        }
    }
}
