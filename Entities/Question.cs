using Entities.Base;
using System;

namespace Entities
{
    public class Question : TrackableEntity<Guid>
    {
        public string Title { get; private set; }

        public string Content { get; private set; }
        
        protected Question() { }

        protected Question(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
