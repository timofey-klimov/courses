using Entities;
using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations.Users
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Metadata
                .FindNavigation(nameof(Student.StudentAssignedTests))
                .SetField("_tests");

            builder.Metadata
                .FindNavigation(nameof(Student.StudentStudyGroups))
                .SetField("_groups");

            builder.Metadata
                .FindNavigation(nameof(Student.StudentTeachers))
                .SetField("_studentTeachers");

        }
    }
}
