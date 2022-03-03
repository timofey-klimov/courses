using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Test : TrackableEntity<Guid>
    {
        public User CreatedBy { get; private set; }

        private List<Question> _questions;

        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

        private Test() { }

        public Test(User createdBy, ICollection<Question> questions)
        {
            _questions = questions.ToList();
            CreatedBy = createdBy;
        }
    }
}
