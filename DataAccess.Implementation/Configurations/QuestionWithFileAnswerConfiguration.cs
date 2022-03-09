using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class QuestionWithFileAnswerConfiguration : IEntityTypeConfiguration<QuestionWithFileAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionWithFileAnswer> builder)
        {
        }
    }
}
