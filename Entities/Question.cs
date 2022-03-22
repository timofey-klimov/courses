using Entities.Base;
using System;

namespace Entities
{
    public class Question : AuditableEntity<long>
    {
        public int Position { get; private set; }

        public string Content { get; private set; }
        
        protected Question() { }

        protected Question(string content, int position)
        {
            Content = content;
            Position = position;
        }
    }
}
