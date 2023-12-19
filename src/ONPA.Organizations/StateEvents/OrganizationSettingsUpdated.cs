﻿using ONPA.Common.Domain;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.StateEvents;

public sealed class OrganizationSettingsUpdated : StateEventBase
{
    public OrganizationSettings Settings { get; private set; }

    public OrganizationSettingsUpdated(OrganizationSettings settings)
    {
        this.Settings = settings;
    }
    
    private OrganizationSettingsUpdated(Guid tenantId, Guid aggregateId, long aggregateVersion, OrganizationSettings settings) : base(tenantId, aggregateId,aggregateVersion)
    {
        this.Settings = settings;
    }
    
    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new OrganizationSettingsUpdated(tenantId, aggregateId, aggregateVersion, this.Settings);
    }
}