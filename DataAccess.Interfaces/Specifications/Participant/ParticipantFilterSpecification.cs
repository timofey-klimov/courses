using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class ParticipantFilterSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        private readonly string _name;
        private readonly string _surname;
        private readonly string _login;
        private readonly bool _isOnlyActive;

        public ParticipantFilterSpecification(string name, string surname, string login, bool isOnlyActive)
        {
            _name = name;
            _surname = surname;
            _login = login;
            _isOnlyActive = isOnlyActive;
        }

        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            ISpecification<Entities.Participants.Participant> compositSpec = default;

            if (_name != null)
            {
                compositSpec = new ParticipantNameSpecification(_name);
            }

            if (_surname != null)
            {
                var spec = new ParticipantSurnameSpecification(_surname);
                compositSpec = compositSpec == null ? spec : compositSpec.Or(spec);
            }

            if (_login != null)
            {
                var spec = new ParticipantLoginSpecification(_login);
                compositSpec = compositSpec == null ? spec : compositSpec.Or(spec);
            }

            if (_isOnlyActive)
            {
                var spec = new OnlyActiveParticipantSpecification(true);
                compositSpec = compositSpec == null ? spec : compositSpec.And(spec);
            }

            return compositSpec?.CreateCriteria();
        }
    }
}
