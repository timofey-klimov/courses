using Authorization.Interfaces;
using MediatR;
using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.User.Model;

namespace UseCases.User.Queries.GetUserRole
{
    public class GetUserRoleHandler : IRequestHandler<GetUserRoleRequest, IEnumerable<string>>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetUserRoleHandler(ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
        }
        public async Task<IEnumerable<string>> Handle(GetUserRoleRequest request, CancellationToken cancellationToken)
        {
            return _currentUserProvider.GetUserRoles().Select(x => x.Role);
        }
    }
}
