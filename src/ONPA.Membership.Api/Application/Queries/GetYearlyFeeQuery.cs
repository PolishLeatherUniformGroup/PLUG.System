using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetYearlyFeeQuery : ApplicationQueryBase<SingleResult<YearlyFeeResult>>
{
    public GetYearlyFeeQuery(Guid tenantId, int year): base(tenantId)
    {
        this.Year = year;
    }

    public int Year { get; init; }
}