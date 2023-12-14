namespace ONPA.Organizations.Infrastructure.ReadModel;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string CardPrefix { get;  set; }
    public string TaxId { get;  set; }
    public string AccountNumber { get;  set; }
    public string Address { get;  set; }
    public string? Regon { get;  set; }
    public string ContactEmail { get;  set; }
}