using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberExpelAppealReceived : StateEventBase
{
    public MembershipExpel Expel { get; private set; }

    public MemberExpelAppealReceived(MembershipExpel expel)
    {
        this.Expel = expel;
    }

    private MemberExpelAppealReceived(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberExpelAppealReceived(tenantId,aggregateId, aggregateVersion, this.Expel);
    }
}