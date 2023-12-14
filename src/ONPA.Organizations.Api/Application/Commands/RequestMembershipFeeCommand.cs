using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record RequestMembershipFeeCommand : ApplicationCommandBase
{
    public Guid OrganizationId { get; init; }
    public int Year { get; init; }
    public Money Amount { get; init; }

    public RequestMembershipFeeCommand(Guid organizationId, int year, Money amount)
    {
        this.OrganizationId = organizationId;
        this.Year = year;
        this.Amount = amount;
    }
}