using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Database.Configurations
{
    public class MemberSuspensionConfiguration : IEntityTypeConfiguration<MemberSuspension>
    {
        public void Configure(EntityTypeBuilder<MemberSuspension> builder)
        {
            builder.ToTable(nameof(MemberSuspension), "membership");
            builder.HasKey(x => x.MemberId);
            builder.HasKey(x => x.SuspensionDate);
            builder.Property(x => x.Justification).IsRequired();
            builder.Property(x => x.MemberId).IsRequired();
            builder.Property(x => x.TenantId).IsRequired();
            builder.Property(x => x.SuspensionDate).IsRequired();
            builder.Property(x => x.SuspendedUntil).IsRequired();
            builder.Property(x => x.AppealDate);
            builder.Property(x => x.AppealJustification);
            builder.Property(x => x.AppealDecisionDate);
        }
    }
}
