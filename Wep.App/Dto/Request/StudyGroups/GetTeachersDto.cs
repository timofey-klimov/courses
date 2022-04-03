using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wep.App.Dto.Request.StudyGroups
{
    public class GetTeachersDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Limit { get; set; } = 5;

        public int Offset { get; set; } = 0;
    }
}
