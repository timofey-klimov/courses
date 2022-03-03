using MediatR;
using UseCases.Common.User.Model;

namespace UseCases.User.Queries.GetUserRole
{
    public class GetUserRoleRequest : IRequest<UserRole>
    {
    }
}
