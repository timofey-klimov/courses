using System;

namespace Entities.Users.States
{
    public class BlockedState : State
    {
        public BlockedState(Participant participant) 
            : base(participant)
        {
        }

        public override void Activate(string hashedPassword)
        {
            throw new NotImplementedException();
        }

        public override void BlockParticipant()
        {
            throw new NotImplementedException();
        }

        public override void UnBlockParticipant()
        {
            ChangeState(ParticipantState.Active);
        }
    }
}
