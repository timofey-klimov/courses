using System;

namespace Entities.Base
{
    public interface IAutitableEntity
    {
        public DateTime CreateDate { get; }

        public DateTime? UpdateDate { get; }

        public void Create(int createdBy);

        public void Update(int updatedBy);
       
    }
}
