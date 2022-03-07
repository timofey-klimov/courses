using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Test.Dto;

namespace Wep.App.Dto.Request.Test
{
    public class CreateTestDto
    {
        public string Title { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }
    }
}
