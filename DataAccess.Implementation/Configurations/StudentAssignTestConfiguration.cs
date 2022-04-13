using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Implementation.Configurations
{
    public class StudentAssignTestConfiguration : IEntityTypeConfiguration<StudentAssignTest>
    {
        public void Configure(EntityTypeBuilder<StudentAssignTest> builder)
        {
            builder.HasKey(x => new { x.AssignTestId, x.StudentId });

            builder.Ignore(x => x.Id);

            builder.HasOne(x => x.AssignedTest)
                .WithMany(x => x.StudentAssignTests)
                .HasForeignKey(x => x.AssignTestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentAssignedTests)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.ToTable("StudentAssignTests");
        }
    }
}
