namespace PLUG.System.Membership.Api.Application.Queries.Results;

public record YearlyFeeResult
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public YearlyFeeResult(decimal amount, string currency)
    {
        this.Amount = amount;
        this.Currency = currency;
    }
}