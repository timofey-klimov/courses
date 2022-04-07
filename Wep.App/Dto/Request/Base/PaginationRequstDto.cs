using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wep.App.Dto.Request.Base
{
    public class PaginationRequstDto
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;
    }
}
