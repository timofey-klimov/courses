using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Participant.Services
{
    public interface IParticipantFactory
    {
        Task<Entities.Participants.Participant> CreateStudent(string login, string name, string surname, string password, string hashedPassword);
        Task<Entities.Participants.Participant> CreateTeacher(string login, string name, string surname, string password, string hashedPassword);
        Task<Entities.Participants.Participant> CreateAdmin(string login, string name, string surname, string password, string hashedPassword);
    }
}
