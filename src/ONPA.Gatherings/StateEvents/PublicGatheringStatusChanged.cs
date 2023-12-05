﻿using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class PublicGatheringStatusChanged :StateEventBase
{
    public PublicGatheringStatus Status { get; private set; }

    public PublicGatheringStatusChanged(PublicGatheringStatus status)
    {
        this.Status = status;
    }

    private PublicGatheringStatusChanged(Guid aggregateId, long aggregateVersion, PublicGatheringStatus status) : base(aggregateId, aggregateVersion)
    {
        this.Status = status;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringStatusChanged(aggregateId, aggregateVersion, this.Status);
    }
}