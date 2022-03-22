using DataAccess.Interfaces;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UseCases.User.Service
{
    public class ParticipantFactory : IParticipantFactory
    {
        private readonly IDbContext _context;
        public ParticipantFactory(IDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Participant> CreateAdmin(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Role == "Admin");

            var admin = new Admin(login, name, surname, password, hashedPassword, userRole);

            return admin;
        }

        public async Task<Participant> CreateManager(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Role == "Manager");

            var manager = new Manager(login, name, surname, password, hashedPassword, userRole);

            return manager;
        }

        public async Task<Participant> CreateUser(string login, string name, string surname, string password, string hashedPassword)
        {
            var userRole = await _context.Roles.FirstOrDefaultAsync(x => x.Role == "User");

            var user = new Entities.Users.User(login, name, surname, password, hashedPassword, userRole);

            return user;
        }
    }
}
