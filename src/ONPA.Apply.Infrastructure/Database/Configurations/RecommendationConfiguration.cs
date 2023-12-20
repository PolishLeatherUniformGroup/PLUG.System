using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Apply.Infrastructure.ReadModel;

namespace ONPA.Apply.Infrastructure.Database.Configurations;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.ToTable("recommendations", "apply");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.ApplicationId).IsRequired();
        builder.Property(x => x.RecommendingMemberId).IsRequired();
        builder.Property(x => x.RecommendingMemberNumber).IsRequired();
        builder.Property(x => x.RequestDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();
    }
}