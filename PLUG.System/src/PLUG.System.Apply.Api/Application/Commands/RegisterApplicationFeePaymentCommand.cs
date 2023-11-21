using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Api.Application.Commands;

public sealed record RegisterApplicationFeePaymentCommand(Guid ApplicationId, Money Paid, DateTime PaidDate, int DaysToDecision) : ApplicationCommandBase
{
    public Guid ApplicationId { get; private set; } = ApplicationId;
    public Money Paid { get; private set; } = Paid;
    public DateTime PaidDate { get; private set; } = PaidDate;
    public int DaysToDecision { get; private set; } = DaysToDecision;
}