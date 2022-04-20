using Entities.Participants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UseCases.Common.Services.Abstract.Mapper;
using UseCases.Teachers.Dto;

namespace UseCases.Common.Services.Implementation
{
    public class StudentMapper : IStudentMapper
    {
        private readonly IAssignTestMapper _mapper;
        public StudentMapper(IAssignTestMapper assignTestMapper)
        {
            _mapper = assignTestMapper ?? throw new ArgumentNullException(nameof(assignTestMapper));
        }
        public StudentInfoDto ToStudentInfo(Student student, IEnumerable<Entities.StudyGroup> groups)
        {
            return new StudentInfoDto(
                new Dto.StudentDto(student.Id, student.Name, student.Surname, student.Login),
                student.AssignedTests().Select(x => _mapper.ToAssignTestDto(x)),
                groups.Select(x => new StudyGroup.Dto.SimpleStudyGroupDto(x.Id, x.Title, x.CreateDate))
                );
        }
    }
}
