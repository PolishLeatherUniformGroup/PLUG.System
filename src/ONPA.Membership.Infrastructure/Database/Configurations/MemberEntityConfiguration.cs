using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONPA.Membership.Infrastructure.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONPA.Membership.Infrastructure.Database.Configurations
{
    public class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(nameof(Member), "membership");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.MemberNumber).IsRequired();
            builder.Property(x=>x.Address).IsRequired();
            builder.Property(x=>x.Email).IsRequired();
            builder.Property(x=> x.FirstName).IsRequired();
            builder.Property(x=>x.LastName).IsRequired();
            builder.Property(x=>x.Phone).IsRequired();
            builder.Property(x=>x.Status).IsRequired();
            builder.Property(x=>x.MembershipType).IsRequired();
            builder.Property(x=>x.JoinDate).IsRequired();
            builder.Property(x=>x.OrganizationId).IsRequired();
            builder.Property(x=>x.MembershipValidUntil).IsRequired();
            builder.Property(x => x.TerminationDate);
            builder.Property(x => x.TerminationReason);
        }
    }
}
