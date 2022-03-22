using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content)
                .IsRequired();
            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.HasDiscriminator<string>("Questiontype")
                .HasValue<QuestionWithTextAnswer>("TextAnswer")
                .HasValue<QuestionWithFileAnswer>("FileAnswer")
                .HasValue<QuestionWithAnswerOptions>("AnswerOptions");

            builder.ToTable("Questions");
        }
    }
}
