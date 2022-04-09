using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class StudyGroupConfiguration : IEntityTypeConfiguration<StudyGroup>
    {
        public void Configure(EntityTypeBuilder<StudyGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.HasMany(x => x.AssignedTests)
                .WithOne();

            builder.Metadata.FindNavigation(nameof(StudyGroup.Students))
                .SetField("_students");

            builder.Metadata.FindNavigation(nameof(StudyGroup.AssignedTests))
                .SetField("_assignTests");
        }
    }
}
