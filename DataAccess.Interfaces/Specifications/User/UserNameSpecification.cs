using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.User
{
    public class UserNameSpecification : BaseSpecification<Entities.Users.User>
    {
        private readonly string _name;
        public UserNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Entities.Users.User, bool>> CreateCriteria()
        {
            return x => x.Name == _name;
        }
    }
}
