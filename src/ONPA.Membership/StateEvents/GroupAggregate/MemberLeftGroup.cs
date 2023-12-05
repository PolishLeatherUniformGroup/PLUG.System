using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public class MemberLeftGroup : StateEventBase
{
    public Guid GroupMemberId { get; private set; }
    public DateTime LeaveDate { get; private set; }

    public MemberLeftGroup(Guid groupMemberId, DateTime leaveDate)
    {
        this.GroupMemberId = groupMemberId;
        this.LeaveDate = leaveDate;
    }

    private MemberLeftGroup(Guid aggregateId, long aggregateVersion, Guid groupMemberId,
        DateTime leaveDate) : base(aggregateId, aggregateVersion)
    {
        this.GroupMemberId = groupMemberId;
        this.LeaveDate = leaveDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberLeftGroup(aggregateId, aggregateVersion, this.GroupMemberId, this.LeaveDate);
    }
}