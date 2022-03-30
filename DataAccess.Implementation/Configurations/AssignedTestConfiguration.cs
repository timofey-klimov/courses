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

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.Property(x => x.CompletedDate)
                .HasColumnType("datetime2(0)");

            builder.HasOne(x => x.Test)
                .WithMany();

            builder.ToTable("AssignedTests");
        }
    }
}
