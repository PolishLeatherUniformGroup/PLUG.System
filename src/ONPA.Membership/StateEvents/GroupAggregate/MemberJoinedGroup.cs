using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public partial class MemberJoinedGroup : StateEventBase
{
    public GroupMember Member { get; private set; }

    public MemberJoinedGroup(GroupMember member)
    {
        this.Member = member;
    }

    private MemberJoinedGroup(Guid tenantId, Guid aggregateId, long aggregateVersion, GroupMember member) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Member = member;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberJoinedGroup(tenantId, aggregateId, aggregateVersion, this.Member);
    }
}