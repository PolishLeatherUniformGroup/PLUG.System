using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AcceptExpelAppealCommand(Guid TenantId, Guid MemberId, DateTime DecisionDate, string Justification): ApplicationCommandBase(TenantId);