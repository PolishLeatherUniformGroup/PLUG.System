﻿using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMemberExpelsQuery : ApplicationCollectionQueryBase<MemberSuspensionResult>
{
    public Guid MemberId { get; init; }
    public GetMemberExpelsQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}