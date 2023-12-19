using ONPA.Common.Domain;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.StateEvents;

public sealed class OrganizationCreated : StateEventBase
{
    public string Name { get; private set; }
    public string CardPrefix { get; private set; }
    public string TaxId { get; private set; }
    public string AccountNumber { get; private set; }
    public string Address { get; private set; }
    public string ContactEmail { get; private set; }
    public string? Regon { get; private set; }
    public OrganizationSettings Settings { get; private set; }
    
    public OrganizationCreated(string name, string cardPrefix, string taxId, string accountNumber, string address,
        string contactEmail, OrganizationSettings settings, string? regon = null)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.ContactEmail = contactEmail;
        this.Settings = settings;

        this.Regon = regon;
    }
    
    private OrganizationCreated(Guid tenantId, Guid aggregateId, long aggregateVersion, string name, string cardPrefix, string taxId, string accountNumber, string address, string contactEmail, OrganizationSettings settings, string? regon = null) : base(tenantId, aggregateId,aggregateVersion)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.Settings = settings;
        this.Regon = regon;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new OrganizationCreated(tenantId, aggregateId, aggregateVersion, this.Name, this.CardPrefix, this.TaxId, this.AccountNumber, this.Address, this.ContactEmail,this.Settings,this.Regon);
    }
}