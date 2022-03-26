using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.User
{
    public class ParticipantNameSpecification : BaseSpecification<Entities.Participants.Participant>
    {
        private readonly string _name;
        public ParticipantNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Entities.Participants.Participant, bool>> CreateCriteria()
        {
            return x => x.Name == _name;
        }
    }
}
