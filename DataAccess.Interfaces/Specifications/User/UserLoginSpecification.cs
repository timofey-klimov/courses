﻿using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.User
{
    public class UserLoginSpecification : BaseSpecification<Entities.Users.User>
    {
        private readonly string _login;
        public UserLoginSpecification(string login)
        {
            _login = login;
        }
        public override Expression<Func<Entities.Users.User, bool>> CreateCriteria()
        {
            return x => x.Login == _login;
        }
    }
}
