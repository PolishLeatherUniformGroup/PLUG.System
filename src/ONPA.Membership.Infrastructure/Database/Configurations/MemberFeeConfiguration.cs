using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Database.Configurations
{
    public class MemberFeeConfiguration : IEntityTypeConfiguration<MemberFee>
    {
        public void Configure(EntityTypeBuilder<MemberFee> builder)
        {
            builder.ToTable(nameof(MemberFee), "membership");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=>x.TenantId).IsRequired();
            builder.Property(x=>x.MemberId).IsRequired();
            builder.Property(x=>x.DueDate).IsRequired();
            builder.Property(x=>x.DueAmount).IsRequired();
            builder.Property(x => x.PaidAmount);
            builder.Property(x=>x.Currency).IsRequired();
            builder.Property(x=>x.FeeEndDate).IsRequired();
        }
    }
}
