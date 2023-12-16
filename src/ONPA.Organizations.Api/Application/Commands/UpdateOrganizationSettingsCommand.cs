using ONPA.Common.Application;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record UpdateOrganizationSettingsCommand( Guid OrganizationId, int RequiredRecommendations, int DaysForAppeal, int FeePaymentMonth) : ApplicationCommandBase(OrganizationId);