using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetAllActiveRegularMembersQuery : ApplicationQueryBase<CollectionResult<MemberIdResult>>
{
    public int Page { get; init; } = 0;
    public int PageSize { get; init; } = 20;

    public GetAllActiveRegularMembersQuery(int page, int pageSize)
    {
        this.Page = page;
        this.PageSize = pageSize;
    }
}