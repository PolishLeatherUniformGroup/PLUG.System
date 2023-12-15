using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMemberExpelsQuery : ApplicationCollectionQueryBase<MemberSuspensionResult>
{
    public Guid MemberId { get; init; }
    public GetMemberExpelsQuery(Guid tenantId,Guid memberId, int page, int limit) : base(tenantId,page, limit)
    {
        this.MemberId = memberId;
    }
}