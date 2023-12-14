using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record SuspendMemberCommand(Guid TenantId, Guid MemberId, string Justification, DateTime SuspendDate, DateTime SuspendUntil, int DaysToAppeal) : ApplicationCommandBase(TenantId);

