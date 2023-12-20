namespace ONPA.Apply.Infrastructure.ReadModel;

public class ApplicationForm
{
    public Guid TenantId { get; set; }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public decimal? RequiredFeeAmount { get; set; }
    public decimal? PaidFeeAmount { get; set; }
    public string? FeeCurrency { get; set; }
}