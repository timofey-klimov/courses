using System;
using System.Collections.Generic;

namespace Wep.App.Dto.Request.Students
{
    public class AssignTestRequest
    {
        public IEnumerable<int> Students { get; set; }

        public DateTime Deadline { get; set; }
    }
}
