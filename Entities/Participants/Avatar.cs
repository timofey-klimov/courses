using Entities.Base;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Participants
{
    public class Avatar : AuditableEntity<int>
    {
        public string Name { get; private set; }

        public byte[] Content { get; private set; }

        public ICollection<Participant> Participants { get; private set; }
       
        private Avatar() { }

        public Avatar(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
    }
}
