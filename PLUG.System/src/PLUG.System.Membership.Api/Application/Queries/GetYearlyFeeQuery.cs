using PLUG.System.Common.Application;
using PLUG.System.Membership.Api.Application.Queries.Results;

namespace PLUG.System.Membership.Api.Application.Queries;

public record GetYearlyFeeQuery : ApplicationQueryBase<SingleResult<YearlyFeeResult>>
{
    public GetYearlyFeeQuery(int year)
    {
        this.Year = year;
    }

    public int Year { get; init; }
}