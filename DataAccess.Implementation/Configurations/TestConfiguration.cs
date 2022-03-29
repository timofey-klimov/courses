using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Implementation.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.HasMany(x => x.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Metadata
                .FindNavigation(nameof(Test.Questions))
                .SetField("_questions");

            builder.ToTable("Tests");

        }
    }
}
