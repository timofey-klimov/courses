using System.Collections;
using System.Collections.Generic;

namespace UseCases.StudyGroup.Dto
{
    public record CreateStudyGroupDto(StudyGroupDto studyGroup, IEnumerable<int> Students);
}
