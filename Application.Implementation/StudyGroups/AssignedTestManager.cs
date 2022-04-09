using Application.Interfaces.StudyGroups;
using Entities;
using System;
using System.Linq;

namespace Application.Implementation.StudyGroups
{
    public class AssignedTestManager : IAssignedTestManager
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
    }
}
