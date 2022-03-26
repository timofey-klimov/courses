using Entities.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Implementation.Configurations.Users
{
    public class ParticipantRoleConfiguration : IEntityTypeConfiguration<ParticipantRole>
    {
        public void Configure(EntityTypeBuilder<ParticipantRole> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("UserRoles");
        }
    }
}
