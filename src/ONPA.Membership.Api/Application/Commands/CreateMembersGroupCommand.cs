using ONPA.Common.Application;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record CreateMembersGroupCommand(
    Guid TenantId,
    string GroupName,
    MembersGroupType GroupType,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);