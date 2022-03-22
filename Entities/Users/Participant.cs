using Entities.Base;
using Entities.Events;
using Entities.Events.User;
using Entities.Exceptions;
using Entities.Users.States;
using System;
using System.Collections.Generic;

namespace Entities.Users
{
    public class Participant : AuditableEntity<int>, IDomainEventProvider
    {
        public string Login { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string HashedPassword { get; private set; }

        public Lazy<ActivationState> ActivasionState { get; private set; }

        public ActiveState State { get; private set; }

        public ICollection<UserRole> Roles { get; private set; }

        public ICollection<DomainEvent> Events { get; private set; }

        protected Participant() 
        {
            Events = new List<DomainEvent>();
            ActivasionState = new Lazy<ActivationState>(() => ActivationState.Create(State));
        }

        public Participant(string login, string name, string surname, string password, string hashedPassword, UserRole role) 
            : this()
        {
            Login = login;
            Name = name;
            Surname = surname;
            HashedPassword = hashedPassword;
            State = ActiveState.FirstSign;
            Events.Add(new UserCreatedEvent(login, password));
            Roles = new List<UserRole>() { role };
        }

        public void Activate(string hashedPassword)
        {
            ActivasionState.Value.ChangePassword(hashedPassword, this);

            State = ActiveState.PasswordChanged;
        }

        public void ChangePassword(string hashedPassword)
        {
            if (HashedPassword == hashedPassword)
                throw new PasswordMatchesExcepton();

            HashedPassword = hashedPassword;
        }

    }
}
