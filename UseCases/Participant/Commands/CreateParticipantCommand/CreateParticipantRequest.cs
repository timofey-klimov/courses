using MediatR;
using UseCases.Participant.Dto;

namespace UseCases.Participant.Commands.CreateParticipantCommand
{
    public record CreateParticipantRequest : IRequest<ParticipantDto>
    {
        public string Name { get; }

        public string Surname { get; }

        public string Login { get; }

        public string Password { get; }

        public string UserRole { get; }

        public CreateParticipantRequest(string login, string password, string name, string surname, string role)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            UserRole = role;
        }
    }
}
