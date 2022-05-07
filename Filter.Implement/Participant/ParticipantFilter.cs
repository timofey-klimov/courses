using Entities.Participants.States;
using Filter.Interfaces;
using Filter.Interfaces.FilterTypes;

namespace Filter.Implement.Participant
{
    public class ParticipantFilter : FilterBase<Entities.Participants.Participant>
    {
        public StringFilter Name { get; set; }

        public StringFilter Surname { get; set; }

        public StringFilter Login { get; set; }
    }
}
