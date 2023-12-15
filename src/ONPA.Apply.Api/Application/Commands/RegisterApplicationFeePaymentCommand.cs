using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record RegisterApplicationFeePaymentCommand(Guid TenantId, Guid ApplicationId, Money Paid, DateTime PaidDate, int DaysToDecision) : ApplicationCommandBase(TenantId);