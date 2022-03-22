using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
    {
        public void Configure(EntityTypeBuilder<AnswerOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.Property(x => x.Content)
                .IsRequired();

            builder.Property(x => x.IsCorrect)
                .IsRequired();

            builder.ToTable("AnswerOptions");
        }
    }
}
