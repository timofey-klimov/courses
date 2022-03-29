using System;

namespace Entities.Base
{
    public interface IAutitableEntity
    {
        public void Create(int createdBy);

        public void Update(int updatedBy);
       
    }
}
