using Authorization.Interfaces;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Dto;
using UseCases.Common.Exceptions;
using UseCases.User.Dto;

namespace UseCases.User.Queries.GetUserForPagination
{
    public class GetUsersForPaginationHandler : IRequestHandler<GetUsersForPaginationRequest, Pagination<PaginationUserDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFilterProvider _filterProvider;
        private readonly ICurrentUserProvider _currentUserProvider;

        public GetUsersForPaginationHandler(
            IDbContext dbContext, 
            IMapper mapper, 
            IFilterProvider filterProvider,
            ICurrentUserProvider userProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _filterProvider = filterProvider ?? throw new ArgumentNullException(nameof(filterProvider));
            _currentUserProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task<Pagination<PaginationUserDto>> Handle(GetUsersForPaginationRequest request, CancellationToken cancellationToken)
        {
            var spec = new UserFilterSpecification(request.Name, request.Surname, request.Login);

            var baseQuery =  _dbContext
                .Participants
                .OfType<Entities.Users.User>();

            var count = await _filterProvider.GetCountQuery(baseQuery, spec);

            var users = _filterProvider.GetQuery(baseQuery, spec)
                .Skip(request.Offset)
                .Take(request.Limit)
                .OrderBy(x => x.Id)
                .AsEnumerable();

            return new Pagination<PaginationUserDto>(_mapper.Map<IEnumerable<PaginationUserDto>>(users), count);
        }
    }
}
