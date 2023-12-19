using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record CreateMemberCommand(
    Guid TenantId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Address,
    DateTime JoinDate,
    Money PaidFee,
    string? Operator=null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);