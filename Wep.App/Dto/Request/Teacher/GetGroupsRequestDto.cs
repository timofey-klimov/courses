using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wep.App.Dto.Request.Base;

namespace Wep.App.Dto.Request.Teacher
{
    public class GetGroupsRequestDto : PaginationRequstDto
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
