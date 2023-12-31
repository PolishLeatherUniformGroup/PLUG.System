using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ExpireMembershipCommand(Guid TenantId, Guid MemberId, DateTime ExpirationDate, string Reason) :ApplicationCommandBase(TenantId);