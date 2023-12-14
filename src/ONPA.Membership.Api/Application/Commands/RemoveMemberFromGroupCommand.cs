using ONPA.Common.Application;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record RemoveMemberFromGroupCommand(Guid TenantId, Guid GroupId, CardNumber MemberNumber, DateTime RemoveDate) : ApplicationCommandBase(TenantId);