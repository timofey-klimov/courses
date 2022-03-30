﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class StudentStudyGroupConfiguration : IEntityTypeConfiguration<StudentStudyGroup>
    {
        public void Configure(EntityTypeBuilder<StudentStudyGroup> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.StudyGroupId });

            builder.Ignore(x => x.Id);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudyGroups)
                .HasForeignKey(x => x.StudentId);

            builder.HasOne(x => x.StudyGroup)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.StudyGroupId);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.ToTable("StudentStudyGroups");
        }
    }
}
