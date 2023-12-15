namespace ONPA.Organizations.Infrastructure.ReadModel;

public class OrganizationFee
{
    public Guid OrganizationId { get; set; }
    public int Year { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}