﻿using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class ExpelAppealDismissed : StateEventBase
{
    public MembershipExpel Expel { get; set; }

    public ExpelAppealDismissed(MembershipExpel expel)
    {
        this.Expel = expel;
    }

    private ExpelAppealDismissed(Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ExpelAppealDismissed(aggregateId, aggregateVersion, this.Expel);
    }
}