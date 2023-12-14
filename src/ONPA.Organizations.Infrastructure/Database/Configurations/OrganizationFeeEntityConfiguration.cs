using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Database.Configurations;

public class OrganizationFeeEntityConfiguration : IEntityTypeConfiguration<OrganizationFee>
{
    public void Configure(EntityTypeBuilder<OrganizationFee> builder)
    {
        builder.ToTable("OrganizationFees", schema: "org");
        builder.HasKey(e=>new{e.OrganizationId, e.Year});
        builder.Property(e=>e.OrganizationId).IsRequired();
        builder.Property(e=>e.Year).IsRequired();
        builder.Property(e=>e.Amount).HasPrecision(8,2).IsRequired();
        builder.Property(e=>e.Currency).HasMaxLength(3).IsRequired();
    }
}