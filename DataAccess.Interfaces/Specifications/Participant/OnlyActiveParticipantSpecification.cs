using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class OnlyActiveParticipantSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            return x => x.State != Entities.Participants.States.ParticipantState.Blocked;
        }
    }
}
