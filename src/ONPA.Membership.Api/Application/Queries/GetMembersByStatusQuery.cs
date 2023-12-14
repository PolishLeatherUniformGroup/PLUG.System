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