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
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ApplicationId).HasColumnName("application_id");
        builder.Property(x=>x.RecommendingMemberId).HasColumnName("recommending_member_id");
        builder.Property(x=>x.RecommendingMemberNumber).HasColumnName("recommending_member_number");
        builder.Property(x=>x.RequestDate).HasColumnName("request_date");
        builder.Property(x=>x.Status).HasColumnName("status");
        
    }
}