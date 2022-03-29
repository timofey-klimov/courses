using DataAccess.Interfaces;
using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UseCases.Participant.Services
{
    public class ParticipantFactory : IParticipantFactory
    {
        private readonly IDbContext _context;
        public ParticipantFactory(IDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Entities.Participants.Participant> CreateAdmin(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "Admin");

            var admin = new Admin(login, name, surname, password, hashedPassword, userRole);

            return admin;
        }

        public async Task<Entities.Participants.Participant> CreateTeacher(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "Teacher");

            var manager = new Teacher(login, name, surname, password, hashedPassword, userRole);

            return manager;
        }

        public async Task<Entities.Participants.Participant> CreateStudent(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "Student");

            var user = new Student(login, name, surname, password, hashedPassword, userRole);

            return user;
        }
    }
}
