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
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Phone).HasColumnName("phone");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ApplicationDate).HasColumnName("application_date");
        builder.Property(x => x.LastUpdateDate).HasColumnName("last_update_date");
        builder.Property(x => x.RequiredFeeAmount).HasColumnName("required_fee_amount");
        builder.Property(x => x.PaidFeeAmount).HasColumnName("paid_fee_amount");
        builder.Property(x => x.FeeCurrency).HasColumnName("fee_currency");
        
    }
}