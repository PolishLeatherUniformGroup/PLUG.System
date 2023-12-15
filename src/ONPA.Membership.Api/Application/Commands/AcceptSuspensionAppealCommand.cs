using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AcceptSuspensionAppealCommand(Guid TenantId, Guid MemberId, DateTime DecisionDate, string Justification): ApplicationCommandBase(TenantId);