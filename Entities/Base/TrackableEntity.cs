using System;

namespace Entities.Base
{
    public class TrackableEntity<T> : Entity<T>
        where T: IEquatable<T>
    {
        public DateTime CreateDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public TrackableEntity()
        {
            CreateDate = DateTime.Now;
        }

        public void UpdateEntity()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
