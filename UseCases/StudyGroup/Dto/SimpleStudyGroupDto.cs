using System;

namespace UseCases.StudyGroup.Dto
{
    public class SimpleStudyGroupDto
    {
        public SimpleStudyGroupDto(int id, string title, DateTime createDate)
        {
            Id = id;
            Title = title;
            CreateDate = createDate;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
