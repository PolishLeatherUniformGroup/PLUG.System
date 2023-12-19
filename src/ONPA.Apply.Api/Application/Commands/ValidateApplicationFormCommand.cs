using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Apply.Api.Application.Commands;

public record ValidateApplicationFormCommand(
    Guid TenantId,
    Guid ApplicationId,
    IEnumerable<(string MemberNumber, Guid? MemberId)> Recommenders,
    Money YearlyFee,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId, Operator);
