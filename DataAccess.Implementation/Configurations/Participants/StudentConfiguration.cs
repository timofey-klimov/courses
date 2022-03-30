using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations.Users
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasMany(x => x.AssignedTests)
                .WithOne();

            builder.Metadata
                .FindNavigation(nameof(Student.AssignedTests))
                .SetField("_tests");
        }
    }
}
