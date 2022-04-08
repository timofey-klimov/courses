using DataAccess.Interfaces.Specifications.Base;
using DataAccess.Interfaces.Specifications.StudyGroup.Between;
using DataAccess.Interfaces.Specifications.StudyGroup.Contains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.StudyGroup
{
    public class StudyGroupFilterSpecification : BaseSpecification<Entities.StudyGroup>
    {
        private readonly string _title;
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;
        public StudyGroupFilterSpecification(string title, DateTime? startDate, DateTime? endDate)
        {
            _title = title;
            _startDate = startDate;
            _endDate = endDate;
        }
        public override Expression<Func<Entities.StudyGroup, bool>> CreateCriteria()
        {
            ISpecification<Entities.StudyGroup> compositSpec = default;

            if (_title != null)
            {
                compositSpec = new StudyGroupTitleContainsSpecification(_title);
            }

            if (_startDate != null || _endDate != null)
            {
                var spec = new StudyGroupCreateDataBetweenSpecification(_startDate, _endDate);
                compositSpec = compositSpec == null ? spec : compositSpec.Or(spec);
            }

            return compositSpec?.CreateCriteria() ?? AlwaysTrue();
        }
    }
}
