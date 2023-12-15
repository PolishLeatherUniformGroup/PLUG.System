using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record RequestMemberFeePaymentCommand(Guid TenantId, Guid MemberId, Money FeeAmount, DateTime DueDate, DateTime EndOfPeriod) : ApplicationCommandBase(TenantId);