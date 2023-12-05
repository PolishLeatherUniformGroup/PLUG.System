using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberReactivated : StateEventBase
{
    public MemberReactivated()
    {
    }

    private MemberReactivated(Guid aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberReactivated(aggregateId,aggregateVersion);
    }
}