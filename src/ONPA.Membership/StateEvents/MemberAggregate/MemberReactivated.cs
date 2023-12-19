using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberReactivated : StateEventBase
{
    public MemberReactivated()
    {
    }

    private MemberReactivated(Guid tenantId, Guid aggregateId, long aggregateVersion) : base(tenantId, aggregateId, aggregateVersion)
    {
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberReactivated(tenantId, aggregateId,aggregateVersion);
    }
}