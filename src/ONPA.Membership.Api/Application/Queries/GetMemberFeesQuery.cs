using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMemberFeesQuery : ApplicationCollectionQueryBase<MemberFee>
{
    public Guid MemberId { get; init; }
    public GetMemberFeesQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}