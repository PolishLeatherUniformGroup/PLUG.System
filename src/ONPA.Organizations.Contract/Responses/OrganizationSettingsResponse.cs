namespace ONPA.Organizations.Contract.Responses;

public record OrganizationSettingsResponse(int RequiredRecommendations, int DaysForAppeal, int FeePaymentMonth);