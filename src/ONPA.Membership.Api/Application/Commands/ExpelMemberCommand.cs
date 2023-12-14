using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ExpelMemberCommand(Guid TenantId, Guid MemberId, string Justification, DateTime ExpelDate, int DaysToAppeal) : ApplicationCommandBase(TenantId);