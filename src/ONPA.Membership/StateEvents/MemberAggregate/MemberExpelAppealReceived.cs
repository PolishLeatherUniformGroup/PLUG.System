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

    private MemberExpelAppealReceived(Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberExpelAppealReceived(aggregateId, aggregateVersion, this.Expel);
    }
}