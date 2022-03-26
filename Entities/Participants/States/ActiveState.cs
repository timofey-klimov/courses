using Entities.Exceptions;

namespace Entities.Participants.States
{
    public class ActiveState : State
    {
        public ActiveState(Participant participant)
            : base(participant)
        {
        }

        public override void Activate(string hashedPassword)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockParticipant()
        {
            ChangeState(ParticipantState.Blocked);
        }

        public override void UnBlockParticipant()
        {
            throw new System.NotImplementedException();
        }
    }
}
