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

    public MemberExpelled(Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberExpelled(aggregateId,aggregateVersion,this.Expel);
    }
}