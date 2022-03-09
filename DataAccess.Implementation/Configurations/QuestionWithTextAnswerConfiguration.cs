using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class QuestionWithTextAnswerConfiguration : IEntityTypeConfiguration<QuestionWithTextAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionWithTextAnswer> builder)
        {
        }
    }
}
