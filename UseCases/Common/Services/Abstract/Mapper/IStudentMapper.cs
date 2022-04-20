using Entities.Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Teachers.Dto;

namespace UseCases.Common.Services.Abstract.Mapper
{
    public interface IStudentMapper
    {
        StudentInfoDto ToStudentInfo(Student student);
    }
}
