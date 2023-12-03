using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

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