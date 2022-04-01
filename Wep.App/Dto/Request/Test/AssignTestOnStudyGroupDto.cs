using System;

namespace Wep.App.Dto.Request.Test
{
    public class AssignTestOnStudyGroupDto
    {
        public int TestId { get; set; }

        public int GroupId { get; set; }

        public DateTime Deadline { get; set; }
    }
}
