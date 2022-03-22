using Entities.Events;
using System.Collections.Generic;

namespace Entities.Base
{
    public interface IDomainEventProvider
    {
        ICollection<DomainEvent> Events { get; }
    }
        
}
