using Entities;
using Entities.Participants;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.Interfaces.StudyGroups
{
    public interface IStudyGroupService
    {
        void AssignTestOnStudyGroup(AssignedTest test, StudyGroup group, Action<string> loggerFactory);
        StudyGroup CreateStudyGroup(IEnumerable<Student> students, Teacher teacher, string title);
    }
}
