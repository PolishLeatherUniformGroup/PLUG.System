using ONPA.Common.Application;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record UpdateOrganizationSettingsCommand(Guid TenantId, Guid OrganizationId, int RequiredRecommendations, int DaysForAppeal, int FeePaymentMonth) : ApplicationCommandBase(TenantId);