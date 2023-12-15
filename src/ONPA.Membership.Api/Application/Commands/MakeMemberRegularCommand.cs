using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MakeMemberRegularCommand(Guid TenantId, Guid MemberId) : ApplicationCommandBase(TenantId);