using ONPA.Common.Domain;
using ONPA.Organizations.DomainEvents;
using ONPA.Organizations.StateEvents;
using PLUG.System.SharedDomain;
using PLUG.System.SharedDomain.Helpers;

namespace ONPA.Organizations.Domain;

public sealed class Organization : AggregateRoot
{
    public string Name { get; private set; }
    public string CardPrefix { get; private set; }
    public string TaxId { get; private set; }
    public string AccountNumber { get; private set; }
    public string Address { get; private set; }
    public string? Regon { get; private set; }
    
    public string ContactEmail { get; private set; }
    
    public OrganizationSettings Settings { get; private set; } = OrganizationSettings.Default;
    
    private readonly List<MembershipFee> _membershipFees = new();
    public IEnumerable<MembershipFee> MembershipFees => this._membershipFees.AsReadOnly();

    public Organization(Guid aggregateId, IEnumerable<IStateEvent> changes, string contactEmail) : base(aggregateId, changes)
    {
        this.ContactEmail = contactEmail;
    }
    
    public Organization(string name, string cardPrefix, string taxId, string accountNumber, string address, string contactEmail, string? regon = null)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.ContactEmail = contactEmail;
        this.Regon = regon;
        
        var change = new OrganizationCreated(name, cardPrefix, taxId, accountNumber, address,contactEmail, regon);
        this.RaiseChangeEvent(change);
    }
    
    public void UpdateSettings(OrganizationSettings settings)
    {
        this.Settings = settings;
        var change = new OrganizationSettingsUpdated(settings);
        this.RaiseChangeEvent(change);
    }
    
    public void UpdateOrganizationData(string name, string cardPrefix, string taxId, string accountNumber, string address, string contactEmail, string? regon = null)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        
        this.Regon = regon;
        
        var change = new OrganizationDataUpdated(name, cardPrefix, taxId, accountNumber, address,contactEmail, regon);
        this.RaiseChangeEvent(change);
    }
    
    public void RequestMembershipFee(int year,  Money amount)
    {
        var fee = new MembershipFee(year, amount);
        this._membershipFees.Add(fee);
        
        var change = new MembershipFeeRequested(fee);
        this.RaiseChangeEvent(change);
        
        var buildFeePaymentDeadline = DateTime.Now.MonthEnd(this.Settings.FeePaymentMonth,year);
        var domainEvent = new MembershipFeeRequestedDomainEvent(fee, buildFeePaymentDeadline);
        this.RaiseDomainEvent(domainEvent);
    }
}