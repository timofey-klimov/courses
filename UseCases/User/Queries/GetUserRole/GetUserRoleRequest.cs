using MediatR;
using System.Collections.Generic;

namespace UseCases.User.Queries.GetUserRole
{
    public class GetUserRoleRequest : IRequest<IEnumerable<string>>
    {
    }
}
