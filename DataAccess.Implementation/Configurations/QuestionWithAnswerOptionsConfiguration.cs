using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class QuestionWithAnswerOptionsConfiguration : IEntityTypeConfiguration<QuestionWithAnswerOptions>
    {
        public void Configure(EntityTypeBuilder<QuestionWithAnswerOptions> builder)
        {
            builder.HasMany(x => x.Answers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(QuestionWithAnswerOptions.Answers))
                .SetField("_ansewers");
        }
    }
}
