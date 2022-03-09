using Authorization.Interfaces;
using DataAccess.Interfaces;
using Entities;
using Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Encription;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;
using UseCases.Common.User.Exceptions;
using UseCases.User.Service;

namespace UseCases.User.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IParticipantFactory _participantFactory;
        public CreateUserHandler(IDbContext dbContext, ICurrentUserProvider currentUserProvider, IParticipantFactory factory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserProvider = currentUserProvider ?? throw new ArgumentNullException(nameof(currentUserProvider));
            _participantFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
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

            return Unit.Value;
        }
    }
}
