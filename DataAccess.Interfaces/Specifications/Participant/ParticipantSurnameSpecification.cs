using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class ParticipantSurnameSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        private readonly string _surname;
        public ParticipantSurnameSpecification(string surname)
        {
            _surname = surname;
        }
        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            return x => x.Surname == _surname;
        }
    }
}
