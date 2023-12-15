using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetYearlyFeeQuery : ApplicationQueryBase<SingleResult<YearlyFeeResult>>
{
    public GetYearlyFeeQuery(int year)
    {
        this.Year = year;
    }

    public int Year { get; init; }
}