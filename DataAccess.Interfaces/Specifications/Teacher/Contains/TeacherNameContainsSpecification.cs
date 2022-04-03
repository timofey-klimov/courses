using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Teacher.Contains
{
    public class TeacherNameContainsSpecification : BaseSpecification<Entities.Participants.Teacher>
    {
        private readonly string _name;

        public TeacherNameContainsSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Entities.Participants.Teacher, bool>> CreateCriteria()
        {
            return x => x.Name.Contains(_name);
        }
    }
}
