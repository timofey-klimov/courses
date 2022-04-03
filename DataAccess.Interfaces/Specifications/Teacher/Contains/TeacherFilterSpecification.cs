using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.Teacher.Contains
{
    public class TeacherFilterSpecification : BaseSpecification<Entities.Participants.Teacher>
    {
        private readonly string _name;
        private readonly string _surname;
        public TeacherFilterSpecification(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public override Expression<Func<Entities.Participants.Teacher, bool>> CreateCriteria()
        {
            ISpecification<Entities.Participants.Teacher> compositeSpec = default;

            if (_name != null)
                compositeSpec = compositeSpec == null ? new TeacherNameContainsSpecification(_name) 
                    : compositeSpec.Or(new TeacherNameContainsSpecification(_name));

            if (_surname != null)
                compositeSpec = compositeSpec == null ? new TeacherSurnameContainsSpecification(_surname)
                    : compositeSpec.Or(new TeacherSurnameContainsSpecification(_surname));

            return compositeSpec?.CreateCriteria();

        }
    }
}
