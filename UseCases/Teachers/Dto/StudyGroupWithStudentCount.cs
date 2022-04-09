using System;
using UseCases.StudyGroup.Dto;

namespace UseCases.Teachers.Dto
{
    public class StudyGroupWithStudentCount : SimpleStudyGroupDto
    {
        public int StudentsCount { get; set; }
        public StudyGroupWithStudentCount(int id, string title, DateTime createDate, int studentsCount) 
            : base(id, title, createDate)
        {
            StudentsCount = studentsCount;
        }
    }
}
