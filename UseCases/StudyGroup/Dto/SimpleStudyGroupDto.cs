using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.StudyGroup.Dto
{
    public class SimpleStudyGroupDto
    {
        public SimpleStudyGroupDto(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}
