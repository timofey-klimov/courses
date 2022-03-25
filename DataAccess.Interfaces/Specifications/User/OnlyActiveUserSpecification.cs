using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class OnlyActiveUserSpecification : BaseSpecification<Entities.Users.User>
    {
        public override Expression<Func<Entities.Users.User, bool>> CreateCriteria()
        {
            return x => x.State != Entities.Users.States.ParticipantState.Blocked;
        }
    }
}
