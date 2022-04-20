using Entities.Participants;
using System.Collections;
using System.Collections.Generic;
using UseCases.Teachers.Dto;

namespace UseCases.Common.Services.Abstract.Mapper
{
    public interface IStudentMapper
    {
        StudentInfoDto ToStudentInfo(Student student, IEnumerable<Entities.StudyGroup> groups);
    }
}
