using MediatR;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUser
{
    public class GetUserRequest : IRequest<UserDto>
    {
    }
}
