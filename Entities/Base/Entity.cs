using Entities.Events;
using System;

namespace Entities.Base
{
    public abstract class Entity<T> : BaseEntity
        where T : IEquatable<T>
    {
        public T Id { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (obj is not Entity<T> e) return false;

            return this.Id.Equals(e.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> one, Entity<T> another)
        {
            if (ReferenceEquals(one, null) && ReferenceEquals(another, null)) return true;

            if (ReferenceEquals(one, null) || ReferenceEquals(another, null)) return false;

            return one.Equals(another);
        }

        public static bool operator !=(Entity<T> one, Entity<T> another)
        {
            return !(one == another);
        }
    }
}
