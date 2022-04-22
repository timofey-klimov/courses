using Entities.Base;
using Entities.Events;
using Entities.Events.User;
using Entities.Exceptions;
using Entities.Participants.States;
using System;
using System.Collections.Generic;

namespace Entities.Participants
{
    public class Participant : AuditableEntity<int>, IDomainEventProvider
    {
        public string Login { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string HashedPassword { get; private set; }

        public Avatar Avatar { get; private set; }

        private ParticipantState _state;

        public ParticipantState State => _state;

        public State ActivasionState
        {
            get { return States.State.Create(_state, this); }
        }

        public ParticipantRole Role { get; private set; }

        public ICollection<DomainEvent> Events { get; private set; }

        protected Participant() 
        {
            Events = new List<DomainEvent>();
            
        }

        public Participant(
            string login,
            string name, 
            string surname, 
            string password, 
            string hashedPassword,
            Avatar avatar,
            ParticipantRole role) 
            : this()
        {
            Login = login;
            Name = name;
            Surname = surname;
            HashedPassword = hashedPassword;
            _state = ParticipantState.Created;
            Events.Add(new UserCreatedEvent(login, password));
            Role = role;
            Avatar = avatar;
        }

        public void Activate(string hashedPassword) => ActivasionState.Activate(hashedPassword);
       
        public void Block() => ActivasionState.BlockParticipant();
       
        public void Unblock() => ActivasionState.UnBlockParticipant();


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

        public void ChangeAvatar(byte[] avatar)
        {
            if (avatar == null || avatar.Length == 0)
                throw new ArgumentNullException(nameof(avatar));

            Avatar = new Avatar(Guid.NewGuid().ToString(), avatar);
        }
    }
}
