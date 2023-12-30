using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ONPA.Common.Infrastructure;

public abstract record MultiTenantRequest
{
    [FromHeader(Name = "X-Tenant-Id")]
    public Guid TenantId { get;  set; }

    public dynamic WithTenant(Guid tenantId)
    {
        this.TenantId = tenantId;
        return this;
    }
}