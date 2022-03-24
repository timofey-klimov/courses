using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUser
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IDbContext _dbContext;
        public GetUserRequestHandler(ICurrentUserProvider currentUserProvider, IDbContext dbContext)
        {
            _currentUserProvider = currentUserProvider;
            _dbContext = dbContext;
        }
        public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var userId = _currentUserProvider.GetUserId();

            var user = await _dbContext.Participants
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user.IsBlocked())
                throw new UserBlockedException();

            return new UserDto(user.Name, user.Surname, user.Login, user.Roles.Select(x => x.Role), user.GetState());
        }
    }
}
