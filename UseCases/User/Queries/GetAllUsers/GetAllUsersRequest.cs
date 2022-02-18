using MediatR;
using System.Collections.Generic;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetAllUsers
{
    public class GetAllUsersRequest : IRequest<IEnumerable<UserDto>>
    {

    }
}
