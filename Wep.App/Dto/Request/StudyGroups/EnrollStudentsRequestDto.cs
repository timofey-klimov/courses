using System.Collections.Generic;

namespace Wep.App.Dto.Request.StudyGroups
{
    public class EnrollStudentsRequestDto
    {
        public int Group { get; set; }

        public IEnumerable<int> Students { get; set; }
    }
}
