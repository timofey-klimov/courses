using System;

namespace Entities.Base
{
    public class AuditableEntity<T> : Entity<T>, IAutitableEntity
        where T: IEquatable<T>
    {
        public DateTime CreateDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public int? UpdatedBy { get; private set; }

        public int CreatedBy { get; private set; }


        public void Create(int createdBy)
        {
            CreateDate = DateTime.Now;
            CreatedBy = createdBy;
        }

        public void Update(int updatedBy)
        {
            UpdateDate = DateTime.Now;
            UpdatedBy = updatedBy;
        }
    }
}
