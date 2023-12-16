using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record RequestMembershipFeeCommand(Guid OrganizationId, int Year, Money Amount) : ApplicationCommandBase(OrganizationId);