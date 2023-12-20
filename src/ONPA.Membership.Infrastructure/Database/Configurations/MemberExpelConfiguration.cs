using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Database.Configurations
{
    public class MemberExpelConfiguration : IEntityTypeConfiguration<MemberExpel>
    {
        public void Configure(EntityTypeBuilder<MemberExpel> builder)
        {
            builder.ToTable(nameof(MemberExpel), "membership");
            builder.HasKey(x => x.MemberId);
            builder.HasKey(x => x.ExpelDate);
            builder.Property(x=>x.Justification).IsRequired();
            builder.Property(x => x.MemberId).IsRequired(true);
            builder.Property(x=>x.ExpelDate).IsRequired(true);
            builder.Property(x => x.AppealDate);
            builder.Property(x => x.AppealJustification);
            builder.Property(x => x.AppealDecisionDate);
            
        }
    }
}
