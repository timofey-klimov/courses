using Entities;
using System;

namespace Application.Interfaces.StudyGroups
{
    public interface IAssignedTestManager
    {
        void AssignTestOnStudyGroup(AssignedTest test, StudyGroup group, Action<string> loggerFactory);
    }
}
