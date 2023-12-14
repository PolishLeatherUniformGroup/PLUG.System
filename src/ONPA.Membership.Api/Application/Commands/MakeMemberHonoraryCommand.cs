using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MakeMemberHonoraryCommand(Guid TenantId, Guid MemberId) : ApplicationCommandBase(TenantId);