namespace ONPA.Organizations.Infrastructure.ReadModel;

public class OrganizationSettings
{
    public Guid OrganizationId { get; set; }
    public int RequiredRecommendations { get;  set; }
    public int DaysForAppeal { get;  set; }
    public int FeePaymentMonth { get;  set; }
}