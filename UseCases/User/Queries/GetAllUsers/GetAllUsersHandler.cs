using DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<UserDto>>
    {
        private readonly IDbContext _dbContext;

        public GetAllUsersHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.Users.Select(x => new UserDto { Name = x.Name, Surname = x.Surname,}).ToArray();
        }
    }
}
