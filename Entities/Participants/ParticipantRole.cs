using Entities.Base;

namespace Entities.Participants
{
    public class ParticipantRole : Entity<int>
    {
        public string Name { get; private set; }

        private ParticipantRole() { }

        public ParticipantRole(string name)
        {
            Name = name;
        }
    }
}
