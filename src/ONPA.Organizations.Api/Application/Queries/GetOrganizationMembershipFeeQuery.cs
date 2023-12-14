﻿using System.Linq.Expressions;
using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationMembershipFeeQuery(Guid OrganizationId, int Year) : ApplicationQueryBase<OrganizationFeeResponse?>
{
    public Expression<Func<OrganizationFee, bool>> AsFilter()
    {
        return x => x.OrganizationId == this.OrganizationId && x.Year == this.Year;
    }
    
    public string ToQueryString()
    {
        return $"{OrganizationId}/fees/{Year}";
    }
}