using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Database.Configurations;

public class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations", schema: "org");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e=>e.Name).HasMaxLength(300).IsRequired();
        builder.Property(e=>e.TaxId).HasMaxLength(30).IsRequired();
        builder.Property(e=>e.Address).HasMaxLength(500).IsRequired();
        builder.Property(e=>e.AccountNumber).HasMaxLength(30).IsRequired();
        builder.Property(e=>e.CardPrefix).HasMaxLength(5).IsRequired();
        builder.Property(e=>e.ContactEmail).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Regon).HasMaxLength(30);
    }
}