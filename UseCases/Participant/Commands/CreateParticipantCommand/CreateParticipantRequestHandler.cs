using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Encription;
using System;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Participant.Dto;
using UseCases.Participant.Exceptions;
using UseCases.Participant.Services;

namespace UseCases.Participant.Commands.CreateParticipantCommand
{
    public class CreateParticipantRequestHandler : IRequestHandler<CreateParticipantRequest, ParticipantDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IParticipantFactory _participantFactory;
        public CreateParticipantRequestHandler(IDbContext dbContext, IParticipantFactory participantFactory)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _participantFactory = participantFactory ?? throw new ArgumentNullException(nameof(participantFactory));

        }
        public async Task<ParticipantDto> Handle(CreateParticipantRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = Sha256Encription.Encript(request.Password);

            var user = await _dbContext.Participants
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashedPassword, cancellationToken);

            if (user is not null)
                throw new ParticipantAlreadyExistException();

            if (await _dbContext.Participants.AnyAsync(x => x.Login == request.Login))
                throw new LoginIsNotAvailableException();

            Entities.Participants.Participant participant = default;

            switch (request.UserRole)
            {
                case "Admin":
                    participant = await _participantFactory.CreateAdmin(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
                case "Teacher":
                    participant = await _participantFactory.CreateTeacher(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
                case "Student":
                    participant = await _participantFactory.CreateStudent(request.Login, request.Name, request.Surname, request.Password, hashedPassword);
                    break;
            }

            _dbContext.Participants.Add(participant);

            await _dbContext.SaveChangesAsync();

            return new ParticipantDto(participant.Id, participant.Login, participant.Name, participant.Surname, participant.GetState(), participant.Role.Name);
         }
    }
}
