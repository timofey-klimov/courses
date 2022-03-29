using Entities.Participants;
using Entities.Participants.States;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Implementation.Configurations.Users
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Surname)
                .IsRequired();

            builder.Property(x => x.HashedPassword)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(0)");


            builder.Property(x => x.State)
                .HasConversion(x => x.ToString(), x => (ParticipantState)Enum.Parse(typeof(ParticipantState), x));

            builder.Metadata.FindProperty("State")
                .SetField("_state");
                

            builder.Ignore(x => x.ActivasionState);
            builder.Ignore(x => x.Events);


            builder.HasDiscriminator<string>("ParticipantType")
                .HasValue<Admin>("Admin")
                .HasValue<Teacher>("Teacher")
                .HasValue<Student>("Student");

            builder.ToTable("Participants");
        }
    }
}
