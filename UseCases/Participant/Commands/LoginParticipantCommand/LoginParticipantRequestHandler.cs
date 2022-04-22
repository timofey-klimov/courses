using Authorization.Interfaces;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Encription;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Common.Participant;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Commands.LoginParticipantCommand
{
    public class LoginParticipantRequestHandler : IRequestHandler<LoginParticipantRequest, LoginResultDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public LoginParticipantRequestHandler(IDbContext dbContext, IJwtTokenProvider tokenProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _jwtTokenProvider  = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));

        }

        public async Task<LoginResultDto> Handle(LoginParticipantRequest request, CancellationToken cancellationToken)
        {
            var hashPassword = Sha256Encription.Encript(request.Password);

            var participant = await _dbContext.Participants
                .Include(x => x.Avatar)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Login == request.Login && x.HashedPassword == hashPassword);

            if (participant is null)
                throw new ParticipantNotFoundException();

            var claims = new List<Claim>()
            {
                new Claim("id", participant.Id.ToString()),
                new Claim(ClaimTypes.Role, participant.Role.Name)
            };

            var participantDto = new ParticipantDto
                (participant.Id,participant.Login, participant.Name, participant.Surname, participant.GetState(), participant.Role.Name);

            return new LoginResultDto(_jwtTokenProvider.CreateToken(claims), participantDto);
        }
    }
}
