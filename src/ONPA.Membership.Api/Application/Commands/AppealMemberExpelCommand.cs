using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AppealMemberExpelCommand(Guid TenantId, Guid MemberId, string Justification, DateTime AppealDate) : ApplicationCommandBase(TenantId);