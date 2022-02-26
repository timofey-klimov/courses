using Entities.Events;
using System.Collections.Generic;

namespace Entities.Base
{
    public class DomainEventsEntity
    {
        public ICollection<DomainEvent> Events { get; private set; }

        public DomainEventsEntity()
        {
            Events = new List<DomainEvent>();
        }
    }
}
