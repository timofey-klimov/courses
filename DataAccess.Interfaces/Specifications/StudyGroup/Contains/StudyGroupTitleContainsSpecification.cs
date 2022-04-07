using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.StudyGroup.Contains
{
    public class StudyGroupTitleContainsSpecification : BaseSpecification<Entities.StudyGroup>
    {
        private readonly string _title;
        public StudyGroupTitleContainsSpecification(string title)
        {
            _title = title;
        }
        public override Expression<Func<Entities.StudyGroup, bool>> CreateCriteria()
        {
            return x => x.Title.Contains(_title);
        }
    }
}
