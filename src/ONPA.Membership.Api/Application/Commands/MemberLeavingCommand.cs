using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MemberLeavingCommand(Guid TenantId, Guid MemberId, DateTime LeaveDate) :ApplicationCommandBase(TenantId);