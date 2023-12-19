using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record CreateApplicationFormCommand(
    Guid TenantId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    List<string> Recommendations,
    string Address,
    string? Operator = null)
    : MultiTenantApplicationCommandBase(TenantId,Operator);