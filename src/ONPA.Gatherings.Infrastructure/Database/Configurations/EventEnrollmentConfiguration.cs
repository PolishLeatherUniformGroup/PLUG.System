using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Database.Configurations;

public class EventEnrollmentConfiguration : IEntityTypeConfiguration<EventEnrollment>
{
    public void Configure(EntityTypeBuilder<EventEnrollment> builder)
    {
        builder.ToTable("enrollments", "gatherings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.EventId).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PlacesBooked).IsRequired();
        builder.Property(x => x.RegistrationDate).IsRequired();
        builder.Property(x => x.RequiredPaymentAmount).IsRequired();
        builder.Property(x => x.Currency).IsRequired();
        builder.Property(x => x.PaidAmount);
        builder.Property(x => x.PaidDate);
        builder.Property(x => x.CancellationDate);
        builder.Property(x => x.RefundableAmount);
        builder.Property(x => x.RefundedAmount);
        builder.Property(x => x.RefundDate);
        builder.HasMany<EventParticipant>(x=>x.Participants)
            .WithOne().HasForeignKey(x => x.EnrollmentId);
    }
}