using Entities.Base;
using Entities.Events;
using Entities.Events.User;
using Entities.Exceptions;
using Entities.Users.States;
using System.Collections.Generic;

namespace Entities.Users
{
    public class Participant : AuditableEntity<int>, IDomainEventProvider
    {
        public string Login { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string HashedPassword { get; private set; }

        private ParticipantState _state;

        public ParticipantState State => _state;

        public State ActivasionState
        {
            get { return States.State.Create(_state, this); }
        }

        public ICollection<UserRole> Roles { get; private set; }

        public ICollection<DomainEvent> Events { get; private set; }

        protected Participant() 
        {
            Events = new List<DomainEvent>();
            
        }

        public Participant(string login, string name, string surname, string password, string hashedPassword, UserRole role) 
            : this()
        {
            Login = login;
            Name = name;
            Surname = surname;
            HashedPassword = hashedPassword;
            _state = ParticipantState.Created;
            Events.Add(new UserCreatedEvent(login, password));
            Roles = new List<UserRole>() { role };
        }

        public void Activate(string hashedPassword)
        {
            ActivasionState.Activate(hashedPassword);
        }

        public void Block()
        {
            ActivasionState.BlockParticipant();
        }

        public void ChangePassword(string hashedPassword)
        {
            if (HashedPassword == hashedPassword)
                throw new PasswordMatchesExcepton();

            HashedPassword = hashedPassword;
        }

        public string GetState()
        {
            return State.ToString();
        }

        public bool IsBlocked() => _state == ParticipantState.Blocked;
    }
}
