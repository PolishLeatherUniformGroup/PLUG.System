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

    private MemberJoinedGroup(Guid aggregateId, long aggregateVersion, GroupMember member) : base(aggregateId, aggregateVersion)
    {
        this.Member = member;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberJoinedGroup(aggregateId, aggregateVersion, Member);
    }
}