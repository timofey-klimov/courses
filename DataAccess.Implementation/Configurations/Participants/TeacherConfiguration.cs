using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations.Users
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Metadata
                .FindNavigation(nameof(Teacher.CreatedTests))
                .SetField("_tests");

            builder.HasMany(x => x.CreatedTests)
                .WithOne()
                .HasForeignKey(x => x.CreatedBy);

            builder.HasMany(x => x.StudyGroups)
                .WithOne(x => x.Teacher)
                .HasForeignKey("TeacherId");

            builder.Metadata
                .FindNavigation(nameof(Teacher.StudyGroups))
                .SetField("_groups");

            builder.Metadata
                .FindNavigation(nameof(Teacher.StudentTeachers))
                .SetField("_studentTeachers");
        }
    }
}
