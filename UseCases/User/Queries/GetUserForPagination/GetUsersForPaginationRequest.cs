using MediatR;
using UseCases.Common.Dto;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserForPagination
{
    public class GetUsersForPaginationRequest : IRequest<Pagination<PaginationUserDto>>
    {
        public int Offset { get; }

        public int Limit { get; }

        public string Name { get; }

        public string Surname { get; }

        public string Login { get; }

        public bool IsOnlyActive { get; }

        public GetUsersForPaginationRequest(int offset, int limit, string name, string surname, string login, bool isOnlyActive)
        {
            Offset = offset;
            Limit = limit;
            Name = name;
            Surname = surname;
            Login = login;
            IsOnlyActive = isOnlyActive;
        }

    }
}
