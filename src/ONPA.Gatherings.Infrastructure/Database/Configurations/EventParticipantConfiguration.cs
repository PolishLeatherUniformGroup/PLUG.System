using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Database.Configurations;

public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.ToTable("event_participants", "gatherings");
        builder.HasKey(x => x.EnrollmentId);
        builder.HasKey(x => x.Email);
        builder.Property(x => x.EnrollmentId).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
    }
}