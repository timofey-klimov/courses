using Authorization.Interfaces;
using MediatR;
using Shared;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Model;

namespace UseCases.User.Queries.GetUserRole
{
    public class GetUserRoleHandler : IRequestHandler<GetUserRoleRequest, UserRole>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetUserRoleHandler(ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }
        public async Task<UserRole> Handle(GetUserRoleRequest request, CancellationToken cancellationToken)
        {
            var user = await _currentUserProvider.GetUserAsync();

            return user.Role.ToEnum<UserRole>();
        }
    }
}
