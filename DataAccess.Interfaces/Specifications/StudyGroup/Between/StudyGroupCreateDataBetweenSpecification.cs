using DataAccess.Interfaces.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Specifications.StudyGroup.Between
{
    public class StudyGroupCreateDataBetweenSpecification : BaseSpecification<Entities.StudyGroup>
    {
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;
        public StudyGroupCreateDataBetweenSpecification(DateTime? startDate, DateTime? endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }
        public override Expression<Func<Entities.StudyGroup, bool>> CreateCriteria()
        {
            if (_startDate == null && _endDate != null)
                return x => x.CreateDate <= _endDate;
            
            if (_startDate != null && _endDate == null)
                return x => x.CreateDate >= _startDate;

            if (_startDate != null && _endDate != null)
                return x => (x.CreateDate >= _startDate) && (x.CreateDate <= _endDate);

            return default;
        }
    }
}
