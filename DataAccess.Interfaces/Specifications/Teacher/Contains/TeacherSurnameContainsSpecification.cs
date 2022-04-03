using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Teacher.Contains
{
    public class TeacherSurnameContainsSpecification : BaseSpecification<Entities.Participants.Teacher>
    {
        private readonly string _surname;
        public TeacherSurnameContainsSpecification(string surname)
        {
            _surname = surname;
        }

        public override Expression<Func<Entities.Participants.Teacher, bool>> CreateCriteria()
        {
            return x => x.Surname.Contains(_surname);
        }
    }
}
