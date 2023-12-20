using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Apply.Infrastructure.ReadModel;

namespace ONPA.Apply.Infrastructure.Database.Configurations;

public class ApplicationFormConfiguration : IEntityTypeConfiguration<ApplicationForm>
{
    public void Configure(EntityTypeBuilder<ApplicationForm> builder)
    {
        builder.ToTable("application_forms", "apply");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ApplicationDate).IsRequired();
        builder.Property(x => x.LastUpdateDate).IsRequired();
        builder.Property(x => x.RequiredFeeAmount);
        builder.Property(x => x.PaidFeeAmount);
        builder.Property(x => x.FeeCurrency);
        
    }
}