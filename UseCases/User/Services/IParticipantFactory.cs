using Entities.Users;
using System.Threading.Tasks;

namespace UseCases.User.Service
{
    public interface IParticipantFactory
    {
        Task<Participant> CreateUser(string login, string name, string surname, string password, string hashedPassword);
        Task<Participant> CreateManager(string login, string name, string surname, string password, string hashedPassword);
        Task<Participant> CreateAdmin(string login, string name, string surname, string password, string hashedPassword);
    }
}
