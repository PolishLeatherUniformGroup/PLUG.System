using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ModifyMemberContactDataCommand(
    Guid TenantId,
    Guid MemberId,
    string Email,
    string Phone,
    string Address,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,Operator);