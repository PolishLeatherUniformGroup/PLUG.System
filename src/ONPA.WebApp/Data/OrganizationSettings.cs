namespace ONPA.WebApp.Data;

public class OrganizationSettingsData
{
    public Guid Id { get; set; }
    public int RequiredRecomendation { get; set; }
    public int DaysToAppeal { get; set; }
    public int FeePaymentMonth { get; set; }
}