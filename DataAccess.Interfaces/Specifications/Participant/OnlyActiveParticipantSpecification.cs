using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class OnlyActiveParticipantSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        private readonly bool _onlyActive;
        public OnlyActiveParticipantSpecification(bool onlyActive)
        {
            _onlyActive = onlyActive;
        }
        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            return _onlyActive == true
                     ? x => x.State != Entities.Participants.States.ParticipantState.Blocked
                     : AlwaysTrue();
        }
    }
}
