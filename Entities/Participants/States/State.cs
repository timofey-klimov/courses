using System.Reflection;

namespace Entities.Participants.States
{
    public abstract class State
    {
        protected Participant Participant { get; set; }
        public State(Participant participant)
        {
            Participant = participant;
        }

        public abstract void Activate(string hashedPassword);

        public abstract void BlockParticipant();

        public abstract void UnBlockParticipant();

        public static State Create(ParticipantState activeState, Participant participant)
        {
            switch (activeState)
            {
                case ParticipantState.Active:
                    return new ActiveState(participant);

                case ParticipantState.Created:
                    return new CreatedState(participant);

                case ParticipantState.Blocked:
                    return new BlockedState(participant);

                default:
                    throw new System.Exception();
            }
        }

        protected void ChangeState(ParticipantState state)
        {
            var fieldInfo = typeof(Participant).GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(Participant, state);
        }
    }
}
