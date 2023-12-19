using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record RegisterMemberFeePaymentCommand(
    Guid TenantId,
    Guid MemberId,
    Guid FeeId,
    Money FeeAmount,
    DateTime PaidDate,
    string? Operator=null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);