using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMembersByStatusQuery : ApplicationCollectionQueryBase<MemberResult>
{
    public int Status { get; init; }
    public GetMembersByStatusQuery(int status, int page, int limit) : base(page, limit)
    {
        this.Status = status;
    }
}

public record GetMemberByIdQuery(Guid MemberId) : ApplicationQueryBase<MemberResult>;

public record GetMemberFeesQuery : ApplicationCollectionQueryBase<MemberFee>
{
    public Guid MemberId { get; init; }
    public GetMemberFeesQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}
public record GetMemberSuspensionsQuery : ApplicationCollectionQueryBase<MemberSuspensionResult>
{
    public Guid MemberId { get; init; }
    public GetMemberSuspensionsQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}
public record GetMemberExpelsQuery : ApplicationCollectionQueryBase<MemberSuspensionResult>
{
    public Guid MemberId { get; init; }
    public GetMemberExpelsQuery(Guid memberId, int page, int limit) : base(page, limit)
    {
        this.MemberId = memberId;
    }
}