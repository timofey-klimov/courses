using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Surname)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.HashedPassword)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");
        }
    }
}
