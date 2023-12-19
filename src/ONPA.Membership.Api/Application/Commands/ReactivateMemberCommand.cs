using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ReactivateMemberCommand(
    Guid TenantId,
    Guid MemberId,
    string? Operator=null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);