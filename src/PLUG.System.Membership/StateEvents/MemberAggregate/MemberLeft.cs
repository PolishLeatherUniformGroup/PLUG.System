﻿using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberLeft : StateEventBase
{
    public DateTime TerminationDate { get; private set; }
    public string TerminationReason { get; private set; }

    public MemberLeft(DateTime terminationDate, string terminationReason)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    private MemberLeft(Guid aggregateId, long aggregateVersion, DateTime terminationDate, string terminationReason) : base(aggregateId, aggregateVersion)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberLeft(aggregateId, aggregateVersion, this.TerminationDate,this.TerminationReason);
    }
}