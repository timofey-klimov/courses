using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class AssignedTestConfiguration : IEntityTypeConfiguration<AssignedTest>
    {
        public void Configure(EntityTypeBuilder<AssignedTest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.Deadline)
                .HasColumnType("datetime2(0)");

            builder.HasOne(x => x.Test)
                .WithMany();

            builder.Metadata
                .FindNavigation(nameof(AssignedTest.StudentAssignTests))
                .SetField("_studentAssignTest");

            builder.ToTable("AssignedTests");
        }
    }
}
