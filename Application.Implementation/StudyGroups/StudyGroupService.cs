using Application.Interfaces.StudyGroups;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Implementation.StudyGroups
{
    public class StudyGroupService : IStudyGroupService
    {
        public void AssignTestOnStudyGroup(AssignedTest test, StudyGroup group, Action<string> loggerFactory = default)
        {
            var students = group.Students.Select(x => x.Student);

            foreach (var student in students)
            {
                try
                {
                    student.AssignTest(test);
                }
                catch (Exception ex)
                {
                    loggerFactory?.Invoke(ex.Message);
                }
            }
        }

        public StudyGroup CreateStudyGroup(IEnumerable<Entities.Participants.Student> students, Entities.Participants.Teacher teacher, string title)
        {
            var studyGroup = new StudyGroup(title, students);

            teacher.AssignGroup(studyGroup);

            foreach(var student in students)
            {
                teacher.AddStudent(student);
            }

            return studyGroup;
        }
    }
}
