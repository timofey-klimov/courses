using Entities.Base;
using System.Collections.Generic;

namespace Entities.Users
{
    public class UserRole : Entity<int>
    {
        public string Role { get; private set; }

        public ICollection<Participant> Participants { get; private set; }

        private UserRole() { }

        public UserRole(string role)
        {
            Role = role;
            Participants = new List<Participant>();
        }
    }
}
