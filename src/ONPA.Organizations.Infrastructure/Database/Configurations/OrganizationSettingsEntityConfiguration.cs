using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Database.Configurations;

public class OrganizationSettingsEntityConfiguration : IEntityTypeConfiguration<OrganizationSettings>
{
    public void Configure(EntityTypeBuilder<OrganizationSettings> builder)
    {
        builder.ToTable("OrganizationSettings", schema: "org");
        builder.HasKey(e => e.OrganizationId);
        builder.Property(e => e.OrganizationId).IsRequired();
        builder.Property(e => e.RequiredRecommendations).IsRequired();
        builder.Property(e => e.DaysForAppeal).IsRequired();
        builder.Property(e => e.FeePaymentMonth).IsRequired();
    }
}