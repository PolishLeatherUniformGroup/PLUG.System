using ONPA.Common.Application;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AddMemberToGroupCommand(
    Guid TenantId,
    Guid GroupId,
    CardNumber MemberNumber,
    Guid MemberId,
    string FirstName,
    string LastName,
    DateTime JoinDate,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);
