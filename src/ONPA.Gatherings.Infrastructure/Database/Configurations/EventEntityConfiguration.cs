using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Database.Configurations;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(nameof(Event), "gatherings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Regulations).IsRequired();
        builder.Property(x => x.PlannedCapacity);
        builder.Property(x => x.PricePerPerson).IsRequired();
        builder.Property(x => x.Currency).IsRequired();
        builder.Property(x => x.ScheduledStart).IsRequired();
        builder.Property(x => x.EnrollmentDeadline).IsRequired();
        builder.Property(x => x.PublishDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
    }
}