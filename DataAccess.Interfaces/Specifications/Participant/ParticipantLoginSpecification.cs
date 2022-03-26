using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class ParticipantLoginSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        private readonly string _login;
        public ParticipantLoginSpecification(string login)
        {
            _login = login;
        }
        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            return x => x.Login == _login;
        }
    }
}
