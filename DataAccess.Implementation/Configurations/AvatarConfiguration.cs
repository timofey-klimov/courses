using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Implementation.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");

            builder.HasMany(x => x.Participants)
                .WithOne(x => x.Avatar);

            builder.Property(x => x.Content)
                .HasConversion(
                    v => Convert.ToBase64String(v),
                    u => Convert.FromBase64String(u));
        }
    }
}
