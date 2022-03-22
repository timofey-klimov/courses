using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class UserSurnameSpecification : BaseSpecification<Entities.Users.User>
    {
        private readonly string _surname;
        public UserSurnameSpecification(string surname)
        {
            _surname = surname;
        }
        public override Expression<Func<Entities.Users.User, bool>> CreateCriteria()
        {
            return x => x.Surname == _surname;
        }
    }
}
