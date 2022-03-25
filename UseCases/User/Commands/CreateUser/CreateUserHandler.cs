﻿using Authorization.Interfaces;
using AutoMapper;
using DataAccess.Interfaces;
using Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Encription;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Common.User.Exceptions;
using UseCases.User.Dto;
using UseCases.User.Service;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, PaginationUserDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IParticipantFactory _participantFactory;
        private readonly IMapper _mapper;
        public CreateUserHandler(
            IDbContext dbContext, 
            ICurrentUserProvider currentUserProvider, 
            IParticipantFactory factory,
            IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _participantFactory = factory ?? throw new ArgumentNullException(nameof(factory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginationUserDto> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (!_currentUserProvider.IsAdmin())
                throw new AccessDeniedException();

            var hashedPassword = Sha256Encription.Encript(request.Password);

            var user = await _dbContext.Participants
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashedPassword, cancellationToken);

            if (user is not null)
                throw new UserExistedException();

            if (await _dbContext.Participants.AnyAsync(x => x.Login == request.Login))
                throw new LoginIsNotAvailableException();

            Participant participant = default;

            switch (request.UserRole)
            {
                case "Admin":
                    participant = await _participantFactory.CreateAdmin(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
                case "Manager":
                    participant = await _participantFactory.CreateManager(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
                case "User":
                    participant = await _participantFactory.CreateUser(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
            }

            _dbContext.Participants.Add(participant);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PaginationUserDto>(participant);
        }
    }
}
