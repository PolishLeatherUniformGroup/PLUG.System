using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberExpelled : StateEventBase
{
    public MembershipExpel Expel { get; private set; }

    public MemberExpelled(MembershipExpel expel)
    {
        this.Expel = expel;
    }

    private MemberExpelled(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberExpelled(tenantId, aggregateId,aggregateVersion,this.Expel);
    }
}