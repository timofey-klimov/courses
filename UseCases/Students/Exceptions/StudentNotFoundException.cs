using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Students.Exceptions
{
    public class StudentNotFoundException : ApiException
    {
        public StudentNotFoundException()
            : base(ExceptionCodes.StudentNotFound)
        {
        }
    }
}
