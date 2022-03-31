using System.Collections.Generic;

namespace Wep.App.Dto.Request.StudyGroups
{
    public class CreateStudyGroupRequestDto
    {
        public string Title { get; set; }

        public int Teacher { get; set; }

        public IEnumerable<int> Students { get; set; }
    }
}
