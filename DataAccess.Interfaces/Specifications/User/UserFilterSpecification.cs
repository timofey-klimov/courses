using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class UserFilterSpecification : BaseSpecification<Entities.Users.User>
    {
        private readonly string _name;
        private readonly string _surname;
        private readonly string _login;

        public UserFilterSpecification(string name, string surname, string login)
        {
            _name = name;
            _surname = surname;
            _login = login;
        }

        public override Expression<Func<Entities.Users.User, bool>> CreateCriteria()
        {
            ISpecification<Entities.Users.User> compositSpec = default;

            if (_name != null)
            {
                compositSpec = new UserNameSpecification(_name);
            }

            if (_surname != null)
            {
                var spec = new UserSurnameSpecification(_surname);
                compositSpec = compositSpec == null ? spec : compositSpec.Or(spec);
            }

            if (_login != null)
            {
                var spec = new UserLoginSpecification(_login);
                compositSpec = compositSpec == null ? spec : compositSpec.Or(spec);
            }

            return compositSpec?.CreateCriteria();
        }
    }
}
