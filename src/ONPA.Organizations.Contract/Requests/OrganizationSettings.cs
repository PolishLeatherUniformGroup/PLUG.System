namespace ONPA.Organizations.Contract.Requests;

public sealed record OrganizationSettings(int RequiredRecommendations,int DaysForAppeal, int FeePaymentMonth);