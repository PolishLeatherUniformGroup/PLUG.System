using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMemberSuspensionsQuery : ApplicationCollectionQueryBase<MemberSuspensionResult>
{
    public Guid MemberId { get; init; }
    public GetMemberSuspensionsQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}