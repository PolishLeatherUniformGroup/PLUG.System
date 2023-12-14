using ONPA.Common.Application;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record UpdateOrganizationSettingsCommand : ApplicationCommandBase
{
    public Guid OrganizationId { get; private set; }
    public int RequiredRecommendations { get; init; }
    public int DaysForAppeal { get; init; }
    public int FeePaymentMonth { get; init; }

    public UpdateOrganizationSettingsCommand(Guid organizationId, int requiredRecommendations, int daysForAppeal, int feePaymentMonth)
    {
        this.OrganizationId = organizationId;
        this.RequiredRecommendations = requiredRecommendations;
        this.DaysForAppeal = daysForAppeal;
        this.FeePaymentMonth = feePaymentMonth;
    }
}