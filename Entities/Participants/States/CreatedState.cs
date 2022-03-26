using Entities.Exceptions;
using System;

namespace Entities.Participants.States
{
    public class CreatedState : State
    {
        public CreatedState(Participant participant) 
            : base(participant)
        {
        }

        public override void Activate(string hashedPassword)
        {
            Participant.ChangePassword(hashedPassword);
            ChangeState(ParticipantState.Active);
        }

        public override void BlockParticipant()
        {
            ChangeState(ParticipantState.Blocked);
        }

        public override void UnBlockParticipant()
        {
            throw new NotImplementedException();
        }
    }
}
