using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Apply.Infrastructure.ReadModel;

namespace ONPA.Apply.Infrastructure.Database.Configurations;

public class ApplicationActionConfiguration : IEntityTypeConfiguration<ApplicationAction>
{
    public void Configure(EntityTypeBuilder<ApplicationAction> builder)
    {
        builder.ToTable("application_actions", "apply");
        builder.HasKey(x => x.ApplicationId);
        builder.HasKey(x=>x.ActionId);
        builder.Property(x => x.ApplicationId).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.ActionId).IsRequired();
        builder.Property(x => x.DecisionDate).IsRequired();
        builder.Property(x => x.DecisionJustification).IsRequired();
        
    }
}